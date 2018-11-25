var common = {
    isEmpty: function (str) {
        if (str == '' || str == undefined || str == null || str.replace(/(^\s*)|(\s*$)/g, "") == "") {
            return true;
        }
        return false;
    },
    createCheckGroup: function (name, start, end) {
        let html = "<div>";
        let newName = name.substring(0, 1).toUpperCase() + name.substring(1);
        for (let i = start; i <= end; i++) {
            if (i <= 9) {
                i = "0" + i;
            }
            html += "<div class=\"ui child checkbox\" style=\"margin:0 20px 10px 0\">";
            html += "<input type=\"checkbox\" name=\"checkbox" + newName + "\" value=\"" + parseInt(i) + "\">";
            html += "<label>" + i + "</label>";
            html += "</div>";
        }
        html += "</div>";
        return html;
    },
    getRandColor: function () {
        let colors = ["red", "orange", "yellow", "olive", "green", "teal", "blue", "violet", "purple", "pink", "brown"];
        let rand = parseInt(Math.random() * (colors.length), 10);
        return colors[rand];
    },
    getDateTime: function () {
        var now = new Date();
        var year = now.getFullYear();       //年
        var month = now.getMonth() + 1;     //月
        var day = now.getDate();            //日
        var hh = now.getHours();            //时
        var mm = now.getMinutes();          //分
        var ss = now.getSeconds();
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
    },
    escapeHTML: function (a) {
        a = "" + a;
        return a.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&apos;");
    }
};