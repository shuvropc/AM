(function ($) {
    "use strict";

    $("#ArticleGrid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: baseurl + "Article/GetAllArticles",
                    dataType: "json",
                    type: "GET"
                }
            },
            schema: {
                model: {
                    fields: {
                        Id: { type: "number" },
                        ArticleTitle: { type: "string" },
                        Version: { type: "number" },
                        File: { type: "string" },
                        IsApproved: { type: "boolean" }
                    }
                }
            },
            pageSize: 15
        },
        height: 550,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [{
            field: "ArticleTitle",
            title: "Article Title"
        }, {
            field: "Version",
            title: "Version",
            template: "<div class='text-center'>#=Version#</div>"
        },{
            field: "IsApproved",
            title: "Approved",
            template: "#if(IsApproved==true){# #: 'Yes' # #}else {# #: 'No' # #}#"
        },{
            field: "FileName",
            title: "File Name"
        },{
            field: "Id",
            title: 'Action',
            template: "#=fnActionRoleTemplate(Id)#",
            width: 250
        }
        ]
    });

})(jQuery);

function downloadFIle(par) {
    var sampleArr = base64ToArrayBuffer(par);
    saveByteArray("Sample Report", sampleArr);
}

function fnActionRoleTemplate(Id) {
    var vAction = "";
    vAction += "<div style='margin:0 auto; width:150px'>";
    vAction += "<a class='btn btn-primary' href='VerifyArticle?pArticleId=" + Id + "'>Verify</a>";
    vAction += "<a class='btn btn-primary'  style='margin-left:5px' href='Details?pArticleId=" + Id + "'>Details</a>";
    vAction += "</div>";
    return vAction;
}

