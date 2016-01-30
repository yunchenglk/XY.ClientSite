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


var apiurl = "http://api.com/api";
var urlitems = {
    //首页闪图
    syst: "8E325643-94B6-45FD-B608-21C5267FAEBC",
    //关于我们
    gywm: "B1441684-D1E5-4A56-AEF1-5606E4D1F17C",
    //联系我们
    lxwm: "04D5D253-2614-4853-9C3E-734F5B7C38B0",
    //新品推荐
    xptj: "573BBF6D-EEE1-44B1-8EBE-88AA022AB180",
    //私人定制
    srdj: "B170DA2F-B314-45B0-A380-27781E1A98EF"
}