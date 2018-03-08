
Date.prototype.format = function () {
    return this.toLocaleDateString().replace(/\//g, '-');
}

layui.use(['layer', 'laydate'], function () {

    var laydate = layui.laydate;

    //执行一个laydate实例
    laydate.render({
        elem: '#txt_datestart',
        min: '2000-01-01 00:00:00',
        max: '2099-12-31 23:59:59',
        done: function (value) {

            var days = $("#txt_days").val();
            if (!isNaN(days) && days > 0) {
                var nd = new Date(new Date(value).getTime() + 86400000 * (days - 1));
                $('#txt_dateend').val(nd.format());
            }
            else {
                $('#txt_dateend').val(value);
            }
        }
    });

    laydate.render({
        elem: '#txt_dateend',
        min: '2000-01-01 00:00:00',
        max: '2099-12-31 23:59:59',
        done: function (value) {
            var date_s = $("#txt_datestart").val();
            if (date_s != '') {
                var ds = new Date(date_s);
                var de = new Date(value);

                var ms = de - ds;

                if (ms >= 0) {
                    $("#txt_days").val(parseInt(ms / 86400000) + 1);
                }
            }
        }
    });
});

$(function () {

    $("#btnsave").click(function () {
        var date_s = $('#txt_datestart').val();
        var date_e = $('#txt_dateend').val();
        var days = $('#txt_days').val();

        if (date_s == '') {
            $('#txt_datestart').focus();
            layer.tips('开始时间不能为空', '#txt_datestart');
            return;
        }

        if (date_e == '') {
            $('#txt_dateend').focus();
            layer.tips('结束时间不能为空', '#txt_dateend');
            return;
        }

        var das = new Date(date_s);
        var dae = new Date(date_e);

        var ms = dae - das;
        if (ms < 0) {
            $('#txt_dateend').focus();
            layer.tips('结束时间不能小于开始时间', '#txt_dateend');
            return;
        }
        var iday = parseInt(ms / 86400000) + 1;

        $("#txt_days").val(iday);

        var url = '/Home/SaveMatHoliday';
        var param = { startdate: date_s, enddate: date_e, days: iday };

        $.post(url, param, function (result) {
            if (result != undefined) {
                layer.msg(result, {
                    time: 20000,
                    btn: ['确定']
                });
            }
        });
    });

});