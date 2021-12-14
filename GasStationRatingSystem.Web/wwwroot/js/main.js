(function($) {
    "use strict"
    // you can find mainAppDirection in _Layout.cshtml
    var xopdez = {
        typography: "SansArabiclight",
        version: "light",
        layout: "vertical",
        headerBg: "color_1",
        navheaderBg: "color_3",
        sidebarBg: "color_1",
        sidebarStyle: "full",
        sidebarPosition: "fixed",
        headerPosition: "fixed",
        containerLayout: "wide",
        direction: mainAppDirection
    };
    new dezSettings(xopdez);
    jQuery(window).on('resize',function(){
        new dezSettings(xopdez);
    });



    if($(".select2").length !== 0) {
        $(".select2").select2();
    }
 
    $(".plusmin").on("click", ".plusbtn", function () {
        //console.log(123);
        $(this).parent().siblings(".form-control").val(parseInt($(this).parent().siblings(".form-control").val()) +1 );
    })

    $(".plusmin").on("click", ".minbtn", function () {
       // console.log(123);

        if(parseInt($(this).parent().siblings(".form-control").val()) >0) {
            $(this).parent().siblings(".form-control").val(parseInt($(this).parent().siblings(".form-control").val()) - 1);
        }
    });

    if($(".datepicker").length !== 0) {

          $('.datepicker').pickadate({
              format: 'mm/dd/yyyy',
              monthsFull: ['يناير', 'فبراير', 'مارس', 'ابريل', 'مايو', 'يونية', 'يوليو', 'اغسطس', 'سبتمبر', 'اكتوبر', 'نوفمبر', 'ديسمبر'],
              weekdaysShort: ['الأحد', 'الأثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'],

// Buttons
              today: 'اليوم',
              clear: 'مسح',
              close: 'غلق',

// Accessibility labels
              labelMonthNext: 'الشهر التالي',
              labelMonthPrev: 'الشهر السابق',
              labelMonthSelect: 'اختر شهر',
              labelYearSelect: 'اختر عام',
    });
};
$(".custom-tab-1").on("click",".nav-item",function (){
    $($(this).children(".nav-link").attr("href")).find(".form-control").focusin();
})

})(jQuery);
function setzero(evt){if(evt.keyCode == 8 && $(evt.target).val().length == 1){setTimeout(function(){$(evt.target).val("0")},1)}}
function setone(evt) { if (evt.keyCode == 8 && $(evt.target).val().length == 1) { setTimeout(function () { $(evt.target).val("1") }, 1) } }

// THE SCRIPT THAT CHECKS IF THE KEY PRESSED IS A NUMERIC OR DECIMAL VALUE.
function isNumberWithDotOnly(evt) {


    var element = event.target;
    var val = element.value;
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (
        (charCode != 45 || val.indexOf('-') != -1) &&      // Check minus and only once.
        (charCode != 46 || val.indexOf('.') != -1) &&      // Check dot and only once.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}    

function isNumberKey(evt)
{
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function duplicatetlangtab(langcode,langname){
    $(".custom-tab-1 .nav-tabs").append(`<li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#lang`+langcode+`">`+langname+`</a>
                      </li>`);
    $(".tab-content").append(`<div class="tab-pane fade" id="lang`+langcode + `" data-lang="`+langcode+`">` + $(".tab-content #defaultlang").html() + `</div>`);

    var div = $('#lang' + langcode);
    var txt = div.find('input');

    txt.val('').removeAttr('id').next('span').remove();

    if (typeof makeHelpReadyToUse != undefined) {
        makeHelpReadyToUse();
    }

}
$(document).on('click', '[data-toggle="lightbox"]', function (event) {
    event.preventDefault();
    $(this).ekkoLightbox({ alwaysShowClose: true })
});
$("#FilesModal").on("click", 'img', function () {
    $(".sliderfilesimages .images").empty();
    var xopin = $('#FilesModal img').index(this);
    console.log(xopin);
    $.each($("#FilesModal img"), function (key, value) {
        if (key == xopin) {
            $(".sliderfilesimages .images").append('<img src="' + $(value).attr("src") + '" style="display:block;" />')

        } else {
            $(".sliderfilesimages .images").append('<img src="' + $(value).attr("src") + '" style="display:none;" />')

        }
    })
    $('#modalshowimages').modal({
        show: true
    }); 
})
$("#FilesModal").on("click", '#modalshowimages .rightx', function () {
    if ($("#modalshowimages img").index($("#modalshowimages img:visible")) < ($("#modalshowimages img").length - 1)) {
        $("#modalshowimages img:visible").slideToggle().next().slideToggle();
    } 
})
$("#FilesModal").on("click", '#modalshowimages .leftx', function () {
    if ($("#modalshowimages img").index($("#modalshowimages img:visible")) > 0) {
        $("#modalshowimages img:visible").slideToggle().prev().slideToggle();
    } 


    
})