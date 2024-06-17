using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Linq.Expressions;
using System.Security.Claims;
using HtmlAgilityPack;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizationsDescription;

public class GetOrganizationsDescriptionQueryHandler : IRequestHandler<GetOrganizationsDescriptionQuery, IEnumerable<OrganizationAttributesShortQueryDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
   
    public GetOrganizationsDescriptionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;       
    }

    public async Task<IEnumerable<OrganizationAttributesShortQueryDto>> Handle(GetOrganizationsDescriptionQuery request, CancellationToken cancellationToken)
    {
        var orgsAttributes = await this.UnitOfWork.OrganizationRepository.GetOrganizationsAttibutes(request.NumberOfOrganizations);       

        var orgAttributesShort = orgsAttributes == null ? Enumerable.Empty<OrganizationAttributesShortQueryDto>() :
            this.Mapper.Map<IEnumerable<OrganizationAttributesShortQueryDto>>(orgsAttributes);

        HtmlDocument document = new HtmlDocument();

        foreach (var orgAttribute in orgAttributesShort) 
        {
            document.LoadHtml(orgAttribute.Description);
            StringWriter sw = new StringWriter();
            ConvertTo(document.DocumentNode, sw);
            sw.Flush();
            orgAttribute.Description = sw.ToString().Replace("\r", "");  
        }

        return orgAttributesShort;
    }

    private void ConvertTo(HtmlNode node, TextWriter outText)
    {
        string html;
        switch (node.NodeType)
        {
            case HtmlNodeType.Comment:
                // don't output comments
                break;

            case HtmlNodeType.Document:
                ConvertContentTo(node, outText);
                break;

            case HtmlNodeType.Text:
                // script and style must not be output
                string parentName = node.ParentNode.Name;
                if ((parentName == "script") || (parentName == "style"))
                    break;

                // get text
                html = ((HtmlTextNode)node).Text;

                // is it in fact a special closing node output as text?
                if (HtmlNode.IsOverlappedClosingElement(html))
                    break;

                // check the text is meaningful and not a bunch of whitespaces
                if (html.Trim().Length > 0)
                {
                    outText.Write(HtmlEntity.DeEntitize(html));
                }
                break;

            case HtmlNodeType.Element:
                switch (node.Name)
                {
                    case "p":
                        // treat paragraphs as crlf
                        outText.Write("\r\n");
                        break;
                    case "br":
                        outText.Write("\r\n");
                        break;
                }

                if (node.HasChildNodes)
                {
                    ConvertContentTo(node, outText);
                }
                break;
        }
    }

    private void ConvertContentTo(HtmlNode node, TextWriter outText)
    {
        foreach (HtmlNode subnode in node.ChildNodes)         
            ConvertTo(subnode, outText);        
    }

}

