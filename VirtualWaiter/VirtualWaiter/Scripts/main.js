$(function () {
    leftNavDropdown();
    jQuery('.left-nav').scrollbar();
    toggleNav();
    $('.menu').click(function () {
        $(this).toggleClass('open');
        $(".left-nav").toggleClass('show');
    });
    $(document).ajaxError(function (event, request, settings, thrownError) {
    });

    //Globalize.culture("pl-PL");

    if ($.validator != undefined) {
        $.validator.methods.number = function (value, element) {
            var globalizationValue = value.replace(".", ",");
            return this.optional(element) ||
                !isNaN(Globalize.parseNumber(globalizationValue));
        }

        $.validator.methods.range = function (value, element, param) {
            if (value === undefined && param.length === 2 && isNaN(param[0]) && isNaN(param[1])) {
                return false;
            }
            if (value === "true" && param.length === 2 && isNaN(param[0]) && isNaN(param[1])) {
                return true;
            }

            var globalizationValue = value.replace(".", ",");
            return this.optional(element) || (Globalize.parseNumber(globalizationValue) >= param[0] && Globalize.parseNumber(globalizationValue) <= param[1]);
        }
    }

    var transport = ['webSockets', 'serverSentEvents', 'longPolling', 'foreverFrame'];
    var isChrome = !!window.chrome && !!window.chrome.webstore;
    if (isChrome) {
        transport = ['serverSentEvents', 'longPolling', 'foreverFrame'];
    }
    //// Start the connection.
    //$.connection.hub.start({ transport: transport }).done(function () {
    //    if (hubStarted && typeof hubStarted === "function") {
    //        hubStarted();
    //    }
    //});

    //$.connection.hub.disconnected(function () {
    //    setTimeout(function () {
    //        $.connection.hub.start({ transport: transport });
    //    }, 5000);
    //});
})

$(".left-nav").on('classChange', function () {
    var listEl = $(".left-nav-list-el>a");

    if ($(this).hasClass("narrow")) {
        $('body').on('click', function (e) {
            listEl.parent().removeClass("active")
        })
    } else {
        $('body').off();
    }
})

function leftNavDropdown() {
    var listEl = $(".left-nav-list-el>a");
    listEl.click(function (e) {
        e.stopPropagation();
        if ($(".left-nav").hasClass("narrow")) {
            listEl.parent().removeClass("active")
        }
        $(this).parent().toggleClass("active");
    })
}

function toggleNav() {
    var listEl = $(".left-nav-list-el>a");
    var usernameEl = $(".left-nav-user-name");
    var usernameTmp = $(".left-nav-user-name").text();
    if (usernameTmp) {
        var initials = usernameTmp.split(" ")[0][0] + usernameTmp.split(" ")[1][0]
    }
    $("#toggleNav").click(function () {
        $(".left-nav").toggleClass("narrow").trigger("classChange")
        listEl.parent().removeClass("active")
        if ($(".left-nav").hasClass("narrow")) {
            Cookies.set('narrowNav', "true", {
                expires: 7
            });
            $(".container").removeClass("container-regular").addClass("container-wide");

            usernameEl.text(initials)
            listEl.children().not("span[class*='icon']").hide();
            $(".scroll-wrapper").children().removeClass("scroll-content")
        } else {
            $(".container").removeClass("container-wide").addClass("container-regular");

            usernameEl.text(usernameTmp)
            listEl.children().not("span[class*='icon']").show();
            $(".scroll-wrapper").children().addClass("scroll-content")
        }
    })

}

if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}


function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function TryParseInt(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
}

function TryParseFloat(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            str = str.replace(',', '.');
            if (!isNaN(str)) {
                retValue = parseFloat(str);
            }
        }
    }
    return retValue;
}

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

function getId(url) {
    var videoId = url.split('v=')[1].split(' ')[0];

    videoId = videoId.replace(/\s/g, '');
    if (videoId) {
        return videoId;
    } else {
        return false;
    }
    // var regExp = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;
    //var match = url
    //return (match && match[7].length == 11) ? match[7] : false;
}
if ($.validator != undefined) {

    $.validator.unobtrusive.adapters.add(

        'filesize', ['maxsize'], function (options) {
            options.rules['filesize'] = options.params;
            if (options.message) {
                options.messages['filesize'] = options.message;
            }
        }

    );
    $.validator.unobtrusive.adapters.add(

        'filetype', function (options) {
            options.rules['filetype'] = options.params;
            if (options.message) {
                options.messages['filetype'] = options.message;
            }
        }


    );
    $.validator.unobtrusive.adapters.add(
        'pesel', function (options) {
            options.rules['pesel'] = options.params;
            if (options.message) {
                options.messages['pesel'] = options.message;
            }
        }
    );

    $.validator.unobtrusive.adapters.add(
        'nip', function (options) {
            options.rules['nip'] = options.params;
            if (options.message) {
                options.messages['nip'] = options.message;
            }
        }
    );

    $.validator.unobtrusive.adapters.add(
        'regon', function (options) {
            options.rules['regon'] = options.params;
            if (options.message) {
                options.messages['regon'] = options.message;
            }
        }
    );

    $.validator.addMethod('filesize', function (value, element, params) {
        if (element.files.length < 1) {
            // No files selected
            return true;
        }

        if (!element.files || !element.files[0].size) {
            // This browser doesn't support the HTML5 API
            return true;
        }

        return element.files[0].size < params.maxsize;
    }, '');

    $.validator.addMethod('filetype', function (value, element, params) {
        if (element.files.length < 1) {
            // No files selected
            return true;
        }

        if (!element.files || !element.files[0].size) {
            // This browser doesn't support the HTML5 API
            return true;
        }
        return /^image/.test(element.files[0].type);
    }, '');

    $.validator.addMethod('pesel', function (value, element, params) {
        if (value.length != 11) return true;

        var weights = [1, 3, 7, 9];
        var j = 0;
        var checksum = 0;
        var sum = 0;
        for (var i = 0; i < 10; i++) {
            sum += value[i] * weights[j];
            j++;
            if (j == 4) j = 0;
        }
        checksum = (10 - (sum % 10)) % 10;
        if (checksum == value[10]) {
            return true;
        }
        else {
            return false;
        }
    })

    $.validator.addMethod('nip', function (value, element, params) {
        var re = new RegExp(/[^0-9]/, 'g');
        value = value.replace(re, '');
        if (value.length != 10) return false;

        var weights = [6, 5, 7, 2, 3, 4, 5, 6, 7];
        var checksum = 0;
        var sum = 0;
        for (var i = 0; i < weights.length; i++) {
            sum += value[i] * weights[i];
        }
        checksum = sum % 11;
        if (checksum == value[9]) {
            return true;
        }
        else {
            return false;
        }
    })

    $.validator.addMethod('regon', function (value, element, params) {
        var re = new RegExp(/[^0-9]/, 'g');
        value = value.replace(re, '');
        if (value.length != 9) return false;

        var weights = [8, 9, 2, 3, 4, 5, 6, 7];
        var checksum = 0;
        var sum = 0;
        for (var i = 0; i < weights.length; i++) {
            sum += value[i] * weights[i];
        }
        checksum = (sum % 11) % 10;
        if (checksum == value[8]) {
            return true;
        }
        else {
            return false;
        }
    })
}

function saveColumnGridData(keyName, version, data) {


    data.searchText = "";
    data.pageIndex = 0;
    data.pageSize = 12;
    for (var i = 0; i < data.columns.length; i++) {
        var column = data.columns[i];
        //delete column.sortIndex;
        //delete column.sortOrder;
        delete column.filterValue;
    }
    var jsonData = JSON.stringify(data);
    saveLayoutData(keyName, version, jsonData);
}
function loadGridData(keyName, version) {
    var data = getLayoutData(keyName, version);
    var temp = JSON.parse(data);
    return temp
}

function getLayoutData(keyName, version) {
    var result = null;
    $.ajax({
        url: '/Home/GetLayoutData',
        type: 'post',
        data: {
            key: keyName,
            version: version
        },
        async: false,
        success: function (returnData) {
            if (returnData != "")
                result = returnData;
        },
        complete: function () {
        }
    })
    return result;
}

function saveLayoutData(keyName, version, data) {
    $.ajax({
        url: '/Home/SaveLayoutData',
        type: 'post',
        data: {
            key: keyName,
            version: version,
            data: data
        },
        success: function (returnData) {

        },
        complete: function () {
        }
    })
}


var remoteApp = function (params) {
    var deferred = $.Deferred();
    var result = {};
    for (var i = 0; i < params.rule.parameters.length; i++) {
        var parameter = params.rule.parameters[i];
        if (parameter.IsMain) {
            result[parameter.Name] = params.value;
        }
        else {
            result[parameter.Name] = $("input[name='" + parameter.Name + "']").val();
        }
    }
    $.ajax({
        dataType: "json",
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: params.rule.url,
        data: JSON.stringify(result),
        success: function (data) {
            var valid = Boolean(data);
            if (!valid) {
                params.rule.isValid = false;
                params.validator.validate();
            }
            else {
                params.rule.isValid = true;
                params.validator.validate();
            };
            deferred.resolve(valid);
        },
    });
    return deferred.promise();
}
window.onbeforeunload = function (e) {
    //$.connection.hub.stop();
};

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function showErrorToast(message) {
    $("#errorToast").dxToast({
        message: message,
        type: "error",
        displayTime: 10000,
        closeOnOutsideClick: true,
        visible: true,
        shading: true,
        shadingColor: "#00000077",
    });
}

function showSuccessToast(message) {
    $("#successToast").dxToast({
        message: message,
        type: "success",
        displayTime: 3000,
        closeOnOutsideClick: true,
        visible: true,
        shading: false,
    });
}

function customToast(message, type, displayTime, hideOnClick, width, positionConfig) {
    var doNothideToast = hideOnClick === true;
    var configuration = {
        width: width || 'auto',
        position: positionConfig,
        onHiding: function (e) {
            e.cancel = doNothideToast;
        },
        contentTemplate: function (element) {
            var instance = this;
            element.css({
                display: 'flex',
                alignItems: 'center'
            });
            var icon = element.find('.dx-toast-icon').css({
                flex: '0 0 auto'
            });
            var messageContainer = $('<span>').css({
                flex: '1 1 auto'
            }).addClass('mr-1').text(message).appendTo(element);
            if (hideOnClick) {
                var closeButton = $('<span>').addClass('dx-icon-close').css({
                    display: 'table-cell',
                    verticalAlign: 'middle',
                    cursor: 'pointer',
                    fontSize: '20px',
                    marginLeft: 'auto'
                }).appendTo(element);
                closeButton.click(function () {
                    doNothideToast = false;
                    instance.hide();
                });
            }
        }
    };
    DevExpress.ui.notify(configuration, type || 'info', hideOnClick ? 60 * 60 * 5 : displayTime || 2000);
}
