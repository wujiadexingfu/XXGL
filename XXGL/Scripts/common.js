//!function ($) {
//    $.fn.Overlay = function (show) {
//        return this.each(function () {
//            if (show == 'show') {
//                $(this).addClass('position-relative').append('<div class="widget-box-overlay"><i class=" ace-icon loading-icon fa fa-spinner fa-spin fa-2x white" style="margin-top:10px;"></i></div>');
//            }
//            if (show == 'hide') {
//                $(this).removeClass('position-relative').find('.widget-box-overlay').remove();
//            }
//        });
//    };
//}(jQuery);

//(function ($) {
//    $.fn.extend({
//        foo3: function () {
//            alert('对象级别插件extend方式1');
//        },
//    })
//});

(function ($) {
    $.extend({
        Alert: function (msg, callback, title, isError) {
            var isError = isError || false;
            var classIcon = isError ? 'glyphicon glyphicon-ok   text-success bigger-120' : 'glyphicon glyphicon-ok   text-success bigger-120';
            var title_text = title ? title : "系统信息";
            var title_template = '<h4 class="modal-title"><i class="' + classIcon + '"></i>' + title_text + '</h4>';
            var btn_title = $("<button><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>").addClass("close").attr({ "data-dismiss": "modal" });
            var headerArea = $("<div class='modal-header'></div>").append(btn_title).append(title_template);
            var bodyArea = $("<div class='modal-body'></div>").html(msg);
            var footArea = $("<div class='modal-footer'></div>");
            var btn_OK = $("<button>确定</button>").addClass("btn btn-primary").attr({ "data-dismiss": "modal" }).appendTo(footArea);
            if (callback != undefined) {
                btn_OK.click(function () {
                    callback();
                });
            }
            var contentArea = $("<div id='alertDialog'><div class='modal-dialog'><div class='modal-content'></div></div></div>")
           .find(".modal-content").append(headerArea).append(bodyArea).append(footArea).end()
           .attr({ "tabindex": "-1", "role": "dialog", "aria-labelledby": "myModalLabel", "aria-hidden": "true" }).addClass("modal fade");
            contentArea.appendTo("body").modal('show');
            contentArea.bind('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
                console.log('hidden');
                $(this).remove();
            });
        },

        /*利用了jquery的toastr插件实现四中不同的提示窗风格*/
        
     /*   设置全局的选项*
             toastr.options = {
                "closeButton": true,  //是否带有关闭按钮
                "debug": true,//是否开启Debug调试
                "positionClass": "toast-top-center", //展示的位置
                "onclick": null, //点击事件
                "showDuration": "300",//显示的动画时间
                "progressBar": true, 
                "hideDuration": "1000", //消失的动画时间
                "timeOut": "3000", //展现时间
                "extendedTimeOut": "1000",//加长展示时间
                "showEasing": "swing", //显示时的动画缓冲方式
                "hideEasing": "linear", //消失时的动画缓冲方式
                "showMethod": "fadeIn", //显示时的动画方式
                "hideMethod": "fadeOut"//消失时的动画方式
            };
           /* 调用方式：  toastr.info('内容4', '标题4',messageOpts);*/
       
        InfoToast: function (msg, title) {
            SetToastDefault();
            if (!title) {
                title = "系统信息"
            }
            toastr.info(msg, title);
        },
        SuccessToast: function (msg, title) {

            SetToastDefault();

            if (!title) {
                title = "系统信息"
            }
            toastr.success(msg, title);
        },
        WarningToast: function (msg, title) {
            SetToastDefault();
            if (!!title) {
                title = "系统信息"
            }
            toastr.warning(msg, title);
        },

        ErrorToast: function (msg, title) {
            SetToastDefault();
            if (!title) {
                title = "系统信息"
            }
            toastr.error(msg, title);
        },

        ShowLoading: function (id) {
            $("#" + id).mLoading("show");
        },

        HideLoading: function (id) {
            $("#" + id).mLoading("hide");
        }

    });
})(jQuery);


(function ($) {
    $.fn.extend({

        ///对象方法 显示加载
        ShowLoading: function () {
            $(this).mLoading("show");
        },
        //对象方法 隐藏加载
        HideLoading: function () {
            $(this).mLoading("hide");
        },
    })
})(jQuery);

var SetToastDefault = function () {
    toastr.options = {
        "closeButton": true,  //是否带有关闭按钮
        "debug": true,//是否开启Debug调试
        "positionClass": "toast-top-center", //展示的位置
        "onclick": null, //点击事件
        "showDuration": "300",//显示的动画时间
        "progressBar": true,
        "hideDuration": "1000", //消失的动画时间
        "timeOut": "3000", //展现时间
        "extendedTimeOut": "1000",//加长展示时间
        "showEasing": "swing", //显示时的动画缓冲方式
        "hideEasing": "linear", //消失时的动画缓冲方式
        "showMethod": "fadeIn", //显示时的动画方式
        "hideMethod": "fadeOut"//消失时的动画方式
    };
};