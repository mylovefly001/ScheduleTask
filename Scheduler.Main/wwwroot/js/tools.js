var Tools = /** @class */ (function () {
    function Tools() {
    }
    /**
     * 判断字符串是否为空
     * @param str
     */
    Tools.isEmpty = function (str) {
        if (str == '' || str == undefined || str == null || str.replace(/(^\s*)|(\s*$)/g, "") == "") {
            return true;
        }
        return false;
    };
    /**
     * 生成选择框组
     * @param name
     * @param start
     * @param end
     */
    Tools.createCheckGroup = function (name, start, end) {
        var html = "<div>";
        var newName = name.substring(0, 1).toUpperCase() + name.substring(1);
        for (var i = start; i <= end; i++) {
            var index = i.toString();
            if (i <= 9) {
                index = "0" + index;
            }
            html += "<div class='ui child checkbox' style='margin:0 20px 10px 0'>";
            html += "<input type='checkbox' name=\"checkbox" + newName + "\" value=\"" + i + "\">";
            html += "<label>" + index + "</label>";
            html += "</div>";
        }
        html += "</div>";
        return html;
    };
    /**
     * 获取一个随机颜色
     */
    Tools.randColor = function () {
        var colors = ["red", "orange", "yellow", "olive", "green", "teal", "blue", "violet", "purple", "pink", "brown"];
        var val = Math.random() * colors.length;
        var rand = parseInt(val.toString(), 10);
        return colors[rand];
    };
    /**
     * 获取格式化后的当前时间
     */
    Tools.nowDateTime = function () {
        var now = new Date();
        var year = now.getFullYear(); //年
        var month = now.getMonth() + 1; //月
        var day = now.getDate(); //日
        var hh = now.getHours(); //时
        var mm = now.getMinutes(); //分
        var ss = now.getSeconds(); //秒
        var clock = year + "-";
        if (month < 10) {
            clock += "0";
        }
        clock += month + "-";
        if (day < 10) {
            clock += "0";
        }
        clock += day + " ";
        if (hh < 10) {
            clock += "0";
        }
        clock += hh + ":";
        if (mm < 10) {
            clock += '0';
        }
        clock += mm + ":";
        if (ss < 10) {
            clock += '0';
        }
        clock += ss;
        return (clock);
    };
    /**
     * 获取URL参数
     * @returns {Object}
     */
    Tools.getRequest = function () {
        var url = location.search;
        var request = {};
        if (url.indexOf("?") >= 0) {
            var strs = url.substr(1).split("&");
            for (var i = 0; i < strs.length; i++) {
                var param = strs[i].split("=");
                request[param[0]] = decodeURIComponent(param[1]);
            }
        }
        return request;
    };
    /**
     * 计算页数
     * @param {number} totalNum
     * @param {number} pageNum
     * @returns {number}
     */
    Tools.calcuPage = function (totalNum, pageNum) {
        if (totalNum === void 0) { totalNum = 0; }
        if (pageNum === void 0) { pageNum = 20; }
        return totalNum > pageNum ? parseInt(Math.ceil(totalNum / pageNum).toString()) : 1;
    };
    /**
     * 获取分页
     * @param param
     */
    Tools.getPage = function (param) {
        var mustParam = ["totalRow", "funcName", "pageNum", "currentPage", "container"];
        for (var i = 0; i < mustParam.length; i++) {
            if (!param.hasOwnProperty(mustParam[i])) {
                return false;
            }
        }
        var currentPage = param['currentPage'];
        var totalPage = this.calcuPage(param['"totalRow'], param['pageNum']);
        if (currentPage < 1) {
            currentPage = 1;
        }
        else if (currentPage > totalPage) {
            currentPage = totalPage;
        }
        var num = 7;
        var zj = parseInt((num / 2).toString());
        var kstart = 1;
        var kend = totalPage;
        if (totalPage > num) {
            kend = num;
            if (currentPage > zj + 1 && currentPage < totalPage - zj) {
                kstart = currentPage - zj;
                kend = currentPage + zj;
            }
            else if (currentPage >= totalPage - zj && currentPage <= totalPage) {
                kstart = totalPage - (zj * 2);
                kend = totalPage;
            }
        }
        var pageHtml = "";
        pageHtml += "<div class='ui small basic icon buttons'>";
        if (currentPage > 1) {
            pageHtml += "<button class='ui button' onclick='return " + param["funcName"] + "(1);'><i class='angle double left icon'></i>首页</button>";
            pageHtml += "<button class='ui button' onclick='return " + param["funcName"] + "(" + (currentPage - 1) + ");'><i class='angle left icon'></i>上页</button>";
        }
        else {
            pageHtml += "<button class='ui button' disabled><i class='angle double left icon'></i>首页</button>";
            pageHtml += "<button class='ui button' disabled><i class='angle left icon'></i>上页</button>";
        }
        for (var k = kstart; k <= kend; k++) {
            if (currentPage == k) {
                pageHtml += "<button class='ui button' disabled>" + k + "</button>";
            }
            else {
                pageHtml += "<button class='ui button' onclick='return " + param["funcName"] + "(" + k + ")'>" + k + "</button>";
            }
        }
        if (currentPage < totalPage) {
            pageHtml += "<button class='ui button' onclick='return " + param["funcName"] + "(" + (currentPage + 1) + ")'><i class='angle right icon'></i>下页</button>";
            pageHtml += "<button class='ui button' onclick='return " + param["funcName"] + "(" + totalPage + ")'><i class='angle double right icon'></i>尾页</button>";
        }
        else {
            pageHtml += "<button class='ui button' disabled><i class='angle right icon'></i>下页</button>";
            pageHtml += "<button class='ui button' disabled><i class='angle double right icon'></i>尾页</button>";
        }
        pageHtml += "<button class='ui button' disabled>共 " + param['totalRow'] + " 条数据 ，总页数：" + totalPage + "，当前第 " + currentPage + " 页</button>";
        pageHtml += "</div>";
        $(param["container"]).html(pageHtml);
    };
    /**
     * 获取表格数据
     * @param param = ["layer", "container", "ajaxUrl", "funcName", "page","params","columns","headPage","isPage"]
     */
    Tools.getTableList = function (param) {
        var mustParam = ["layer", "container", "ajaxUrl", "funcName"];
        for (var i = 0; i < mustParam.length; i++) {
            if (!param.hasOwnProperty(mustParam[i])) {
                return false;
            }
        }
        var loadingLayer = param["layer"].load(1);
        var obj = this;
        param["ajaxParam"]["t"] = new Date().getTime();
        $.getJSON(param["ajaxUrl"], param.hasOwnProperty("ajaxParam") ? param["ajaxParam"] : {}, function (data) {
            if (data["code"] > 0) {
                param["layer"].msg(data["msg"], { icon: 2 });
            }
            else {
                var table = param["container"];
                var html = "";
                $.each(data["data"]["list"], function (key, item) {
                    html += "<tr>";
                    $.each(param["columns"], function (i, column) {
                        if (column.hasOwnProperty("fields")) {
                            if (column.hasOwnProperty("event") && typeof (column["event"]) == "function") {
                                try {
                                    if (Tools.isEmpty(column["fields"])) {
                                        html += "<td>" + column["event"](item) + "</td>";
                                    }
                                    else {
                                        html += "<td>" + column["event"](item[column["fields"]]) + "</td>";
                                    }
                                }
                                catch (e) {
                                    html += "<td></td>";
                                }
                            }
                            else if (!Tools.isEmpty(column["fields"])) {
                                html += "<td><label>" + item[column["fields"]] + "</label></td>";
                            }
                        }
                    });
                    html += "</tr>";
                });
                table.find("tbody").html(html);
                var pageHtml = "";
                if (param.hasOwnProperty("isPage") && param["isPage"]) {
                    var currentPage = data["data"]["currentPage"];
                    var totalPage = obj.calcuPage(data["data"]["total"], data["data"]["pageNum"]);
                    if (currentPage < 1) {
                        currentPage = 1;
                    }
                    else if (currentPage > totalPage) {
                        currentPage = totalPage;
                    }
                    var num = 7;
                    var zj = parseInt((num / 2).toString());
                    var kstart = 1;
                    var kend = totalPage;
                    if (totalPage > num) {
                        kend = num;
                        if (currentPage > zj + 1 && currentPage < totalPage - zj) {
                            kstart = currentPage - zj;
                            kend = currentPage + zj;
                        }
                        else if (currentPage >= totalPage - zj && currentPage <= totalPage) {
                            kstart = totalPage - (zj * 2);
                            kend = totalPage;
                        }
                    }
                    pageHtml += "<div class=\"ui small basic icon buttons\">";
                    if (currentPage > 1) {
                        pageHtml += "<button class=\"ui button\" onclick=\"return " + param["funcName"] + "(1);\"><i class=\"angle double left icon\"></i>首页</button>";
                        pageHtml += "<button class=\"ui button\" onclick=\"return " + param["funcName"] + "(" + (currentPage - 1) + ")\"><i class=\"angle left icon\"></i>上页</button>";
                    }
                    else {
                        pageHtml += "<button class=\"ui button\" disabled><i class=\"angle double left icon\"></i>首页</button>";
                        pageHtml += "<button class=\"ui button\" disabled><i class=\"angle left icon\"></i>上页</button>";
                    }
                    for (var k = kstart; k <= kend; k++) {
                        if (currentPage == k) {
                            pageHtml += "<button class=\"ui button\" disabled>" + k + "</button>";
                        }
                        else {
                            pageHtml += "<button class=\"ui button\" onclick=\"return " + param["funcName"] + "(" + k + ")\">" + k + "</button>";
                        }
                    }
                    if (currentPage < totalPage) {
                        pageHtml += "<button class=\"ui button\" onclick=\"return " + param["funcName"] + "(" + (currentPage + 1) + ")\"><i class=\"angle right icon\"></i>下页</button>";
                        pageHtml += "<button class=\"ui button\" onclick=\"return " + param["funcName"] + "(" + totalPage + ")\"><i class=\"angle double right icon\"></i>尾页</button>";
                    }
                    else {
                        pageHtml += "<button class=\"ui button\" disabled><i class=\"angle right icon\"></i>下页</button>";
                        pageHtml += "<button class=\"ui button\" disabled><i class=\"angle double right icon\"></i>尾页</button>";
                    }
                    pageHtml += "<button class=\"ui button\" disabled>共 " + data["data"]["total"] + " 条数据 ，总页数：" + totalPage + "，当前第 " + currentPage + " 页</button>";
                    pageHtml += "</div>";
                }
                var nextPageHtml = table.next();
                if (nextPageHtml.length > 0) {
                    nextPageHtml.remove();
                }
                table.after(pageHtml);
                if (param.hasOwnProperty("headPage") && param["headPage"]) {
                    var prevPageHtml = table.prev();
                    if (prevPageHtml.length > 0) {
                        prevPageHtml.remove();
                    }
                    table.before(pageHtml);
                }
            }
            param["layer"].close(loadingLayer);
        });
    };
    return Tools;
}());
