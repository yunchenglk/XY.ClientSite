(function ($) {
    $.extend({
        GetApiData: function (url) {
            GetDataBefor();
            var resultObj = new Object();
            if ($.trim(url).length == 0) {
                resultObj.data = "错误";
                resultObj.status = 0;
            } else {
                $.ajax({
                    type: "GET",
                    async: false,
                    url: url,
                    data: {
                        v: Math.random()
                    },
                    success: function (result) {
                        resultObj.data = result;
                        resultObj.status = 1;

                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        resultObj.data = "错误";
                        resultObj.status = 0;
                    },
                })
            } return resultObj
        }
    });
    $.fn.bindPage = function (opts) {
        GetDataBefor();
        var obj = $(this);
        var total;
        if (opts == undefined)
            opts = {};
        opts = jQuery.extend({
            url: opts.url == opts.url,
            pageindex: opts.pageindex == undefined ? 1 : opts.pageindex,
            pagesize: opts.pagesize == undefined ? 10 : opts.pagesize,
            where: opts.where == undefined ? "DR|equal|0" : opts.where,
            sortorder: opts.sortorder == undefined ? "" : opts.sortorder,
            sortname: opts.sortname == undefined ? "" : opts.sortname,
            paginationObj: opts.paginationObj == undefined ? ".pagination:first" : opts.paginationObj
        }, opts || {});
        $(obj).html("");
        $.ajax({
            type: "get",
            async: false,
            url: opts.url,
            data: {
                pageIndex: opts.pageindex,
                pageSize: opts.pagesize,
                wheres: opts.where,
                sortorder: opts.sortorder,
                sortname: opts.sortname
            },
            success: function (result) {
                if (result.total > 0) {
                    total = result.total;
                    pageAfter(result, obj);
                } else {
                    $(obj).append("<div>信息更新中...</div>");
                }
            }
        })
        if (opts.pageindex == 1 && total > opts.pagesize) {
            createPage(obj, opts.pageindex, opts.pagesize, total, opts.url, opts.where, opts.sortname, opts.sortorder, opts.paginationObj)
        }
    };
})(jQuery);
function GetDataBefor() {

}
function pageAfter(result, obj) {
    $("#dataModel").tmpl(result.content).appendTo(obj);
}
function createPage(objs, pageIndex, pageSize, total, url, wheres, sortname, sortorder, paginationObj) {
    $(paginationObj).jBootstrapPage({
        pageSize: pageSize,
        total: total,
        maxPageButton: 10,
        onPageClicked: function (obj, pageIndex) {
            $(objs).bindPage({
                url: url,
                pageindex: pageIndex + 1,
                pagesize: pageSize,
                where: wheres,
                sortorder: sortorder,
                sortname: sortname,
                paginationObj: paginationObj
            });
        }
    });
}


var apiurl = "http://api.0359i.com/api";
var urlitems = {
    //关于我们
    gywm: "",
    //产品中心
    cpzx: "",
    //解决方案
    jjfa: "",
    //应用案例
    yyal: "",
    //技术支持
    jszc: "",
    //新闻中心
    xwzx: ""
}