$(function () {
    $("div[name='tab-cron-regex'] .item").tab();
    $("div[data-tab='tab-second']").find("div[bind-value='4']").html(Tools.createCheckGroup("second", 0, 59));
    $("div[data-tab='tab-minute']").find("div[bind-value='4']").html(Tools.createCheckGroup("minute", 0, 59));
    $("div[data-tab='tab-hour']").find("div[bind-value='4']").html(Tools.createCheckGroup("hour", 0, 23));
    $("div[data-tab='tab-day']").find("div[bind-value='6']").html(Tools.createCheckGroup("day", 1, 31));
    $("div[data-tab='tab-month']").find("div[bind-value='5']").html(Tools.createCheckGroup("month", 1, 12));
    $("div[data-tab='tab-week']").find("div[bind-value='6']").html(Tools.createCheckGroup("week", 1, 7));
    $(".ui.radio.checkbox").checkbox({
        onChecked: function () {
            let name = $(this).attr("name");
            switch (name) {
                case "radioSecond":
                    setExpSecond();
                    break;
                case "radioMinute":
                    setExpMinute();
                    break;
                case "radioHour":
                    setExpHour();
                    break;
                case "radioDay":
                    setExpDay();
                    break;
                case "radioMonth":
                    setExpMonth();
                    break;
                case "radioWeek":
                    setExpWeek();
                    break;
                case "radioYear":
                    setExpYear();
                    break;
            }
        }
    });
    //秒相关的设置动作
    $("input[name='inputMinSecond']").change(function () {
        let minSecond = parseInt($(this).val());
        let maxSecond = parseInt($("input[name='inputMaxSecond']").val());
        if (minSecond >= maxSecond) {
            $(this).val(maxSecond - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpSecond();
    });
    $("input[name='inputMaxSecond']").change(function () {
        let maxSecond = parseInt($(this).val());
        let minSecond = parseInt($("input[name='inputMinSecond']").val());
        if (maxSecond <= minSecond) {
            $(this).val(minSecond + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpSecond();
    });
    $("input[name='inputStartSecond']").change(function () {
        let startSecond = parseInt($(this).val());
        let endSecond = parseInt($("input[name='inputEndSecond']").val());
        if (startSecond + endSecond > 59) {
            $(this).val(startSecond - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpSecond();
    });
    $("input[name='inputEndSecond']").change(function () {
        let endSecond = parseInt($(this).val());
        let startSecond = parseInt($("input[name='inputStartSecond']").val());
        if (startSecond + endSecond > 59) {
            $(this).val(endSecond - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpSecond();
    });
    $("input[name='checkboxSecond']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpSecond();
    });


    //分相关的设置动作
    $("input[name='inputMinMinute']").change(function () {
        let minMinute = parseInt($(this).val());
        let maxMinute = parseInt($("input[name='inputMaxMinute']").val());
        if (minMinute >= maxMinute) {
            $(this).val(maxMinute - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMinute();
    });
    $("input[name='inputMaxMinute']").change(function () {
        let maxMinute = parseInt($(this).val());
        let minMinute = parseInt($("input[name='inputMinMinute']").val());
        if (maxMinute <= minMinute) {
            $(this).val(minMinute + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMinute();
    });
    $("input[name='inputStartMinute']").change(function () {
        let startMinute = parseInt($(this).val());
        let endMinute = parseInt($("input[name='inputEndMinute']").val());
        if (startMinute + endMinute > 59) {
            $(this).val(startMinute - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMinute();
    });
    $("input[name='inputEndMinute']").change(function () {
        let endMinute = parseInt($(this).val());
        let startMinute = parseInt($("input[name='inputStartMinute']").val());
        if (startMinute + endMinute > 59) {
            $(this).val(endMinute - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMinute();
    });
    $("input[name='checkboxMinute']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMinute();
    });


    //时相关的设置动作
    $("input[name='inputMinHour']").change(function () {
        let minHour = parseInt($(this).val());
        let maxHour = parseInt($("input[name='inputMaxHour']").val());
        if (minHour >= maxHour) {
            $(this).val(maxHour - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpHour();
    });
    $("input[name='inputMaxHour']").change(function () {
        let maxHour = parseInt($(this).val());
        let minHour = parseInt($("input[name='inputMinHour']").val());
        if (maxHour <= minHour) {
            $(this).val(minHour + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpHour();
    });
    $("input[name='inputStartHour']").change(function () {
        let startHour = parseInt($(this).val());
        let endHour = parseInt($("input[name='inputEndHour']").val());
        if (startHour + endHour > 23) {
            $(this).val(startHour - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpHour();
    });
    $("input[name='inputEndHour']").change(function () {
        let endHour = parseInt($(this).val());
        let startHour = parseInt($("input[name='inputStartHour']").val());
        if (startHour + endHour > 23) {
            $(this).val(endHour - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpHour();
    });
    $("input[name='checkboxHour']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpHour();
    });


    //日相关的设置动作
    $("input[name='inputMinDay']").change(function () {
        let minDay = parseInt($(this).val());
        let maxDay = parseInt($("input[name='inputMaxDay']").val());
        if (minDay >= maxDay) {
            $(this).val(maxDay - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpDay();
    });
    $("input[name='inputMaxDay']").change(function () {
        let maxDay = parseInt($(this).val());
        let minDay = parseInt($("input[name='inputMinDay']").val());
        if (maxDay <= minDay) {
            $(this).val(minDay + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpDay();
    });
    $("input[name='inputStartDay']").change(function () {
        let startDay = parseInt($(this).val());
        let endDay = parseInt($("input[name='inputEndDay']").val());
        if (startDay + endDay > 31) {
            $(this).val(startDay - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpDay();
    });
    $("input[name='inputEndDay']").change(function () {
        let endDay = parseInt($(this).val());
        let startDay = parseInt($("input[name='inputStartDay']").val());
        if (startDay + endDay > 31) {
            $(this).val(endDay - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpDay();
    });
    $("input[name='inputJobDay']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpDay();
    });
    $("input[name='checkboxDay']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpDay();
    });


    //月相关的设置动作
    $("input[name='inputMinMonth']").change(function () {
        let minMonth = parseInt($(this).val());
        let maxMonth = parseInt($("input[name='inputMaxMonth']").val());
        if (minMonth >= maxMonth) {
            $(this).val(maxMonth - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMonth();
    });
    $("input[name='inputMaxMonth']").change(function () {
        let maxMonth = parseInt($(this).val());
        let minMonth = parseInt($("input[name='inputMinMonth']").val());
        if (maxMonth <= minMonth) {
            $(this).val(minMonth + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMonth();
    });
    $("input[name='inputStartMonth']").change(function () {
        let startMonth = parseInt($(this).val());
        let endMonth = parseInt($("input[name='inputEndMonth']").val());
        if (startMonth + endMonth > 12) {
            $(this).val(startMonth - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMonth();
    });
    $("input[name='inputEndMonth']").change(function () {
        let endMonth = parseInt($(this).val());
        let startMonth = parseInt($("input[name='inputStartMonth']").val());
        if (startMonth + endMonth > 12) {
            $(this).val(endMonth - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMonth();
    });
    $("input[name='checkboxMonth']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpMonth();
    });


    //周相关的设置动作
    $("input[name='inputMinWeek']").change(function () {
        let minWeek = parseInt($(this).val());
        let maxWeek = parseInt($("input[name='inputMaxWeek']").val());
        if (minWeek >= maxWeek) {
            $(this).val(maxWeek - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpWeek();
    });
    $("input[name='inputMaxWeek']").change(function () {
        let maxWeek = parseInt($(this).val());
        let minWeek = parseInt($("input[name='inputMinWeek']").val());
        if (maxWeek <= minWeek) {
            $(this).val(minWeek + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpWeek();
    });
    $("input[name='inputStartWeek']").change(function () {
        let startWeek = parseInt($(this).val());
        let endWeek = parseInt($("input[name='inputEndWeek']").val());
        if (startWeek + endWeek > 7) {
            $(this).val(startWeek - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpWeek();
    });
    $("input[name='inputEndWeek']").change(function () {
        let endWeek = parseInt($(this).val());
        let startWeek = parseInt($("input[name='inputStartWeek']").val());
        if (startWeek + endWeek > 7) {
            $(this).val(endWeek - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpWeek();
    });
    $("input[name='inputLastWeek']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpWeek();
    });
    $("input[name='checkboxWeek']").change(function () {
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpWeek();
    });


    $("input[name='inputMinYear']").change(function () {
        let minYear = parseInt($(this).val());
        let maxYear = parseInt($("input[name='inputMaxYear']").val());
        if (minYear >= maxYear) {
            $(this).val(maxYear - 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpYear();
    });
    $("input[name='inputMaxYear']").change(function () {
        let maxYear = parseInt($(this).val());
        let minYear = parseInt($("input[name='inputMinYear']").val());
        if (maxYear <= minYear) {
            $(this).val(minYear + 1);
        }
        $(this).closest(".field").children(".ui.radio.checkbox").checkbox("set checked");
        setExpYear();
    });

    $("input[bind-name='exp']").change(function () {
        getRunCount();
    });

    getCronTab();
});

//#region 具体方法区域
//设置选择秒的规则
function setExpSecond() {
    let checkRadio = parseInt($("input[name='radioSecond']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpSecond']").val("*");
            break;
        case 2:
            let minSecond = $("input[name='inputMinSecond']").val();
            let maxSecond = $("input[name='inputMaxSecond']").val();
            $("input[name='inputExpSecond']").val(minSecond + "-" + maxSecond);
            break;
        case 3:
            let startSecond = $("input[name='inputStartSecond']").val();
            let endSecond = $("input[name='inputEndSecond']").val();
            $("input[name='inputExpSecond']").val(startSecond + "/" + endSecond);
            break;
        case 4:
            let arrSecond = [];
            $.each($("input[name='checkboxSecond']"),
                function (i, item) {
                    if (item.checked) {
                        arrSecond.push($(item).val());
                    }
                });
            $("input[name='inputExpSecond']").val(arrSecond.length > 0 ? arrSecond.join(",") : "*");
            break;
    }
    getCronTab();
}


//设置选择分的规则
function setExpMinute() {
    let checkRadio = parseInt($("input[name='radioMinute']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpMinute']").val("*");
            break;
        case 2:
            let minMinute = $("input[name='inputMinMinute']").val();
            let maxMinute = $("input[name='inputMaxMinute']").val();
            $("input[name='inputExpMinute']").val(minMinute + "-" + maxMinute);
            break;
        case 3:
            let startMinute = $("input[name='inputStartMinute']").val();
            let endMinute = $("input[name='inputEndMinute']").val();
            $("input[name='inputExpMinute']").val(startMinute + "/" + endMinute);
            break;
        case 4:
            let arrMinute = [];
            $.each($("input[name='checkboxMinute']"),
                function (i, item) {
                    if (item.checked) {
                        arrMinute.push($(item).val());
                    }
                });
            $("input[name='inputExpMinute']").val(arrMinute.length > 0 ? arrMinute.join(",") : "*");
            break;
    }
    getCronTab();
}


//设置选择时的规则
function setExpHour() {
    let checkRadio = parseInt($("input[name='radioHour']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpHour']").val("*");
            break;
        case 2:
            let minHour = $("input[name='inputMinHour']").val();
            let maxHour = $("input[name='inputMaxHour']").val();
            $("input[name='inputExpHour']").val(minHour + "-" + maxHour);
            break;
        case 3:
            let startHour = $("input[name='inputStartHour']").val();
            let endHour = $("input[name='inputEndHour']").val();
            $("input[name='inputExpHour']").val(startHour + "/" + endHour);
            break;
        case 4:
            let arrHour = [];
            $.each($("input[name='checkboxHour']"),
                function (i, item) {
                    if (item.checked) {
                        arrHour.push($(item).val());
                    }
                });
            $("input[name='inputExpHour']").val(arrHour.length > 0 ? arrHour.join(",") : "*");
            break;
    }
    getCronTab();
}


//设置选择日的规则
function setExpDay() {
    let checkRadio = parseInt($("input[name='radioDay']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpDay']").val("*");
            break;
        case 2:
            $("input[name='inputExpDay']").val("?");
            break;
        case 3:
            let minDay = $("input[name='inputMinDay']").val();
            let maxDay = $("input[name='inputMaxDay']").val();
            $("input[name='inputExpDay']").val(minDay + "-" + maxDay);
            break;
        case 4:
            let startDay = $("input[name='inputStartDay']").val();
            let endDay = $("input[name='inputEndDay']").val();
            $("input[name='inputExpDay']").val(startDay + "/" + endDay);
            break;
        case 5:
            let jobDay = $("input[name='inputJobDay']").val();
            $("input[name='inputExpDay']").val(jobDay + "W");
            break;
        case 6:
            let arrDay = [];
            $.each($("input[name='checkboxDay']"),
                function (i, item) {
                    if (item.checked) {
                        arrDay.push($(item).val());
                    }
                });
            $("input[name='inputExpDay']").val(arrDay.length > 0 ? arrDay.join(",") : "*");
            break;
    }
    getCronTab();
}

//设置选择月的规则
function setExpMonth() {
    let checkRadio = parseInt($("input[name='radioMonth']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpMonth']").val("*");
            break;
        case 2:
            let weekRadioVal = parseInt($("input[name='radioWeek']:checked").val());
            if (weekRadioVal == 2) {
                $("input[name='radioWeek'][value='1']").closest(".ui.radio.checkbox").checkbox("set checked");
                $("input[name='inputExpWeek']").val("*");
            }
            $("input[name='inputExpMonth']").val("?");
            break;
        case 3:
            let minMonth = $("input[name='inputMinMonth']").val();
            let maxMonth = $("input[name='inputMaxMonth']").val();
            $("input[name='inputExpMonth']").val(minMonth + "-" + maxMonth);
            break;
        case 4:
            let startMonth = $("input[name='inputStartMonth']").val();
            let endMonth = $("input[name='inputEndMonth']").val();
            $("input[name='inputExpMonth']").val(startMonth + "/" + endMonth);
            break;
        case 5:
            let arrMonth = [];
            $.each($("input[name='checkboxMonth']"),
                function (i, item) {
                    if (item.checked) {
                        arrMonth.push($(item).val());
                    }
                });
            $("input[name='inputExpMonth']").val(arrMonth.length > 0 ? arrMonth.join(",") : "*");
            break;
    }
    getCronTab();
}


//设置选择周的规则
function setExpWeek() {
    let checkRadio = parseInt($("input[name='radioWeek']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpWeek']").val("*");
            break;
        case 2:
            let monthRadioVal = parseInt($("input[name='radioMonth']:checked").val());
            if (monthRadioVal == 2) {
                $("input[name='radioMonth'][value='1']").closest(".ui.radio.checkbox").checkbox("set checked");
                $("input[name='inputExpMonth']").val("*");
            }
            $("input[name='inputExpWeek']").val("?");
            break;
        case 3:
            let minWeek = $("input[name='inputMinWeek']").val();
            let maxWeek = $("input[name='inputMaxWeek']").val();
            $("input[name='inputExpWeek']").val(minWeek + "-" + maxWeek);
            break;
        case 4:
            let startWeek = $("input[name='inputStartWeek']").val();
            let endWeek = $("input[name='inputEndWeek']").val();
            $("input[name='inputExpWeek']").val(startWeek + "/" + endWeek);
            break;
        case 5:
            let lastWeek = $("input[name='inputLastWeek']").val();
            $("input[name='inputExpWeek']").val(lastWeek + "L");
            break;
        case 6:
            let arrWeek = [];
            $.each($("input[name='checkboxWeek']"),
                function (i, item) {
                    if (item.checked) {
                        arrWeek.push($(item).val());
                    }
                });
            $("input[name='inputExpWeek']").val(arrWeek.length > 0 ? arrWeek.join(",") : "*");
            break;
    }
    getCronTab();
}

//设置选择年的规则
function setExpYear() {
    let checkRadio = parseInt($("input[name='radioYear']:checked").val());
    switch (checkRadio) {
        case 1:
            $("input[name='inputExpYear']").val("");
            break;
        case 2:
            $("input[name='inputExpYear']").val("*");
            break;
        case 3:
            let minYear = $("input[name='inputMinYear']").val();
            let maxYear = $("input[name='inputMaxYear']").val();
            $("input[name='inputExpYear']").val(minYear + "-" + maxYear);
            break;
    }
    getCronTab();
}
//#endregion

function getCronTab() {
    let second = $("input[name='inputExpSecond']").val();
    let minute = $("input[name='inputExpMinute']").val();
    let hour = $("input[name='inputExpHour']").val();
    let day = $("input[name='inputExpDay']").val();
    let month = $("input[name='inputExpMonth']").val();
    let week = $("input[name='inputExpWeek']").val();
    let year = $("input[name='inputExpYear']").val();
    let result = second + " " + minute + " " + hour + " " + day + " " + month + " " + week;
    if (!Tools.isEmpty(year)) {
        result += " " + year;
    }
    $("input[name='inputExpCron']").val(result);
    getRunCount();
}

function getRunCount() {
    let second = $("input[name='inputExpSecond']").val();
    let minute = $("input[name='inputExpMinute']").val();
    let hour = $("input[name='inputExpHour']").val();
    let day = $("input[name='inputExpDay']").val();
    let month = $("input[name='inputExpMonth']").val();
    let week = $("input[name='inputExpWeek']").val();
    let year = $("input[name='inputExpYear']").val();
    let result = second + "+" + minute + "+" + hour + "+" + day + "+" + month + "+" + week;
    if (!Tools.isEmpty(year)) {
        result += "+" + year;
    }
    $.getJSON("/Trigger/TestRule?count=20&rule=" + encodeURI(result),
        function (rs) {
            let html = "";
            if (rs["code"] > 0) {
                html += rs["msg"];
            } else {
                if (rs["data"].length > 0) {
                    html += "<div class=\"ui list\">";
                    $.each(rs["data"],
                        function (i, item) {
                            html += "<div class=\"item\">" + item + "</div>";
                        });
                    html += "</div>";
                }
            }
            $("td[name='runResult']").html(html);
        });
}

function setConTab(rule) {
    var cronRule = rule.split(" ");
    if (cronRule.length < 6 || cronRule.length > 7) {
        return false;
    }
    var expSecond = cronRule[0];
    var tabSecond = $("div[data-tab='tab-second']:last");
    $("input[name='inputExpSecond']").val(expSecond);
    if (expSecond == "*") {
        $(tabSecond).find(".ui.radio.checkbox:eq(0)").checkbox("set checked");
    } else if (expSecond.search("-") >= 0) {
        $(tabSecond).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
        var list = expSecond.split("-");
        $("input[name='inputMinSecond']").val(list[0]);
        $("input[name='inputMaxSecond']").val(list[1]);
    } else if (expSecond.search("/") >= 0) {
        $(tabSecond).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
        var list = expSecond.split("/");
        $("input[name='inputStartSecond']").val(list[0]);
        $("input[name='inputEndSecond']").val(list[1]);
    } else {
        $(tabSecond).find(".ui.radio.checkbox:eq(3)").checkbox("set checked");
        if (expSecond.search(",") >= 0) {
            var list = expSecond.split(",");
            list.forEach(v => {
                $(tabSecond).find("input[name='checkboxSecond'][value=" + v + "]").closest(".ui.child.checkbox").checkbox("set checked");
            });
        } else {
            $(tabSecond).find("input[name='checkboxSecond'][value=" + expSecond + "]").closest(".ui.child.checkbox").checkbox("set checked");
        }
    }
    var expMinute = cronRule[1];
    var tabMinute = $("div[data-tab='tab-minute']:last");
    $("input[name='inputExpMinute']").val(expMinute);
    if (expMinute == "*") {
        $(tabMinute).find(".ui.radio.checkbox:eq(0)").checkbox("set checked");
    } else if (expMinute.search("-") >= 0) {
        $(tabMinute).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
        var list = expMinute.split("-");
        $("input[name='inputMinMinute']").val(list[0]);
        $("input[name='inputMaxMinute']").val(list[1]);
    } else if (expMinute.search("/") >= 0) {
        $(tabMinute).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
        var list = expMinute.split("/");
        $("input[name='inputStartMinute']").val(list[0]);
        $("input[name='inputEndMinute']").val(list[1]);
    } else {
        $(tabMinute).find(".ui.radio.checkbox:eq(3)").checkbox("set checked");
        if (expMinute.search(",") >= 0) {
            var list = expMinute.split(",");
            list.forEach(v => {
                $(tabMinute).find("input[name='checkboxMinute'][value=" + v + "]").closest(".ui.child.checkbox").checkbox("set checked");
            });
        } else {
            $(tabMinute).find("input[name='checkboxMinute'][value=" + expMinute + "]").closest(".ui.child.checkbox").checkbox("set checked");
        }
    }
    var expHour = cronRule[2];
    var tabHour = $("div[data-tab='tab-hour']:last");
    $("input[name='inputExpHour']").val(expHour);
    if (expHour == "*") {
        $(tabHour).find(".ui.radio.checkbox:eq(0)").checkbox("set checked");
    } else if (expHour.search("-") >= 0) {
        $(tabHour).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
        var list = expHour.split("-");
        $("input[name='inputMinHour']").val(list[0]);
        $("input[name='inputMaxHour']").val(list[1]);
    } else if (expHour.search("/") >= 0) {
        $(tabHour).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
        var list = expHour.split("/");
        $("input[name='inputStartHour']").val(list[0]);
        $("input[name='inputEndHour']").val(list[1]);
    } else {
        $(tabHour).find(".ui.radio.checkbox:eq(3)").checkbox("set checked");
        if (expHour.search(",") >= 0) {
            var list = expHour.split(",");
            list.forEach(v => {
                $(tabHour).find("input[name='checkboxHour'][value=" + v + "]").closest(".ui.child.checkbox").checkbox("set checked");
            });
        } else {
            $(tabHour).find("input[name='checkboxHour'][value=" + expHour + "]").closest(".ui.child.checkbox").checkbox("set checked");
        }
    }
    var expDay = cronRule[3];
    var tabDay = $("div[data-tab='tab-day']:last");
    $("input[name='inputExpDay']").val(expDay);
    if (expDay == "*") {
        $(tabDay).find(".ui.radio.checkbox:eq(0)").checkbox("set checked");
    } else if (expDay == "?") {
        $(tabDay).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
    } else if (expDay.search("-") >= 0) {
        $(tabDay).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
        var list = expDay.split("-");
        $("input[name='inputMinDay']").val(list[0]);
        $("input[name='inputMaxDay']").val(list[1]);
    } else if (expDay.search("/") >= 0) {
        $(tabDay).find(".ui.radio.checkbox:eq(3)").checkbox("set checked");
        var list = expDay.split("/");
        $("input[name='inputStartDay']").val(list[0]);
        $("input[name='inputEndDay']").val(list[1]);
    } else if (expDay.search("W") >= 0) {
        $(tabDay).find(".ui.radio.checkbox:eq(4)").checkbox("set checked");
        $("input[name='inputJobDay']").val(expDay.replace("W", ""));
    } else {
        $(tabDay).find(".ui.radio.checkbox:eq(5)").checkbox("set checked");
        if (expDay.search(",") >= 0) {
            var list = expDay.split(",");
            list.forEach(v => {
                $(tabDay).find("input[name='checkboxDay'][value=" + v + "]").closest(".ui.child.checkbox").checkbox("set checked");
            });
        } else {
            $(tabDay).find("input[name='checkboxDay'][value=" + expDay + "]").closest(".ui.child.checkbox").checkbox("set checked");
        }
    }
    var expMonth = cronRule[4];
    var tabMonth = $("div[data-tab='tab-month']:last");
    $("input[name='inputExpMonth']").val(expMonth);
    if (expMonth == "*") {
        $(tabMonth).find(".ui.radio.checkbox:eq(0)").checkbox("set checked");
    } else if (expMonth == "?") {
        $(tabMonth).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
    } else if (expMonth.search("-") >= 0) {
        $(tabMonth).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
        var list = expMonth.split("-");
        $("input[name='inputMinMonth']").val(list[0]);
        $("input[name='inputMaxMonth']").val(list[1]);
    } else if (expMonth.search("/") >= 0) {
        $(tabMonth).find(".ui.radio.checkbox:eq(3)").checkbox("set checked");
        var list = expMonth.split("/");
        $("input[name='inputStartMonth']").val(list[0]);
        $("input[name='inputEndMonth']").val(list[1]);
    } else {
        $(tabMonth).find(".ui.radio.checkbox:eq(4)").checkbox("set checked");
        if (expMonth.search(",") >= 0) {
            var list = expMonth.split(",");
            list.forEach(v => {
                $(tabMonth).find("input[name='checkboxMonth'][value=" + v + "]").closest(".ui.child.checkbox").checkbox("set checked");
            });
        } else {
            $(tabMonth).find("input[name='checkboxMonth'][value=" + expMonth + "]").closest(".ui.child.checkbox").checkbox("set checked");
        }
    }
    var expWeek = cronRule[5];
    var tabWeek = $("div[data-tab='tab-week']:last");
    $("input[name='inputExpWeek']").val(expWeek);
    if (expWeek == "*") {
        $(tabWeek).find(".ui.radio.checkbox:eq(0)").checkbox("set checked");
    } else if (expWeek == "?") {
        $(tabWeek).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
    } else if (expWeek.search("-") >= 0) {
        $(tabWeek).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
        var list = expWeek.split("-");
        $("input[name='inputMinWeek']").val(list[0]);
        $("input[name='inputMaxWeek']").val(list[1]);
    } else if (expWeek.search("/") >= 0) {
        $(tabWeek).find(".ui.radio.checkbox:eq(3)").checkbox("set checked");
        var list = expWeek.split("/");
        $("input[name='inputStartWeek']").val(list[0]);
        $("input[name='inputEndWeek']").val(list[1]);
    } else if (expWeek.search("L") >= 0) {
        $(tabWeek).find(".ui.radio.checkbox:eq(4)").checkbox("set checked");
        $("input[name='inputLastWeek']").val(expWeek.replace("W", ""));
    } else {
        $(tabWeek).find(".ui.radio.checkbox:eq(5)").checkbox("set checked");
        if (expWeek.search(",") >= 0) {
            var list = expWeek.split(",");
            list.forEach(v => {
                $(tabWeek).find("input[name='checkboxWeek'][value=" + v + "]").closest(".ui.child.checkbox").checkbox("set checked");
            });
        } else {
            $(tabWeek).find("input[name='checkboxWeek'][value=" + expWeek + "]").closest(".ui.child.checkbox").checkbox("set checked");
        }
    }
    console.info(cronRule);
    if (cronRule.length == 7) {
        var expYear = cronRule[6];
        var tabYear = $("div[data-tab='tab-year']:last");
        $("input[name='inputExpYear']").val(expYear);
        if (expYear.search("-") >= 0) {
            $(tabYear).find(".ui.radio.checkbox:eq(2)").checkbox("set checked");
            var list = expYear.split("-");
            $("input[name='inputMinYear']").val(list[0]);
            $("input[name='inputMaxYear']").val(list[1]);
        } else if (expYear == "*") {
            $(tabYear).find(".ui.radio.checkbox:eq(1)").checkbox("set checked");
        }
    }
    $("input[name='inputExpCron']").val(rule);
}