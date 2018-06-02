$(document).ready(function () {
//Start Service Btn

var arrow_width=$(".arrowtop").width();

		$("#CrossPlatformBtn").click(function (e) {
			$("#CrossPlatform").show();
			$("#Enterprise").hide();
			$("#WhyMobiliz").hide();
			var curr_div=e.currentTarget;
			
			var center = find_center(curr_div);
			
			
			
			$(".arrowtop").animate({opacity: 1,left: center-arrow_width/2}, 400);
		});
		$("#EnterpriseBtn").click(function (e) {
			$("#Enterprise").show();
			$("#CrossPlatform").hide();
			$("#WhyMobiliz").hide();
			
			var curr_div=e.currentTarget;
			
			var center = find_center(curr_div);
			
			$(".arrowtop").animate({opacity: 1,left: center-arrow_width/2}, 400);
		});
		$("#WhyMobilizBtn").click(function (e) {
			$("#WhyMobiliz").show();
			$("#Enterprise").hide();
			$("#CrossPlatform").hide();
			
			var curr_div=e.currentTarget;
			
			var center = find_center(curr_div);
			
			$(".arrowtop").animate({opacity: 1,left: center-arrow_width/2}, 400);
		});
		//Mobile Version
		$("#CPlatformBtn").click(function (e) {
			$("#CrossPlatform").show();
			$("#Enterprise").hide();
			$("#WhyMobiliz").hide();
			
			var curr_div=e.currentTarget;
			
			var center = find_center(curr_div);
			
			$(".arrowtop").animate({opacity: 1,left: center-arrow_width/2}, 200);
		});
		$("#EBtn").click(function (e) {
			$("#Enterprise").show();
			
			$("#CrossPlatform").hide();
			$("#WhyMobiliz").hide();
			
			var curr_div=e.currentTarget;
			
			var center = find_center(curr_div);
			
			$(".arrowtop").animate({opacity: 1,left: center-arrow_width/2}, 200);
		});
		$("#WMBtn").click(function (e) {
			$("#WhyMobiliz").show();
			$("#Enterprise").hide();
			$("#CrossPlatform").hide();
			
			var curr_div=e.currentTarget;
			
			var center = find_center(curr_div);
			
			$(".arrowtop").animate({opacity: 1,left: center-arrow_width/2}, 200);
		});
//End Service Btn
	
//Count on Scroll

function find_center(curr_div)
{
	var cross_pos=$(curr_div).position();
			var cross_left=cross_pos.left;
			var cross_width=$(curr_div).width();
			var cross_center=cross_left+(cross_width/2);
			return cross_center;
}


	var position=$('.counterAera').position().top;
	$(window).scroll(function() {
		if ($(window).scrollTop() > position-$(window).height() && $(window).scrollTop() < position) {
			var cnt = 0, cnt2 = 0, cnt3 = 0, cnt4 = 2013, cnt5 = 0, cnt6 = 0 ;
			if (scrolled){
			var count = setInterval(function()
			{
				if (cnt >= 0 && cnt <=6){ $('.counterAera .counterblock .app').html(cnt+'+');  cnt=cnt+2; }
					else{clearInterval(count);}
				}, 2);
			var count2 = setInterval(function()
			{
				if (cnt2 >= 0 && cnt2 <=11){ $('.counterAera .counterblock .team').html(cnt2+'+');  cnt2=cnt2+2; }
					else{clearInterval(count2);}
			}, 2);
			var count3 = setInterval(function()
			{
				if (cnt3 >= 0 && cnt3 <=16){ $('.counterAera .counterblock .testing').html(cnt3+'+');  cnt3=cnt3+2; }
					else{clearInterval(count3);}
			}, 20);
			var count4 = setInterval(function()
			{
				if (cnt4 >= 1 && cnt4 <=4){ $('.counterAera .counterblock .awards').html(cnt4);  cnt4=cnt4-1; }
					else{clearInterval(count4);}
			}, 200);
			scrolled=false;
			}
		}
	else {
		scrolled=true;
		}
	});
//End Count on Scroll
// Reach Us PopUp
	$('#btnRech').click(function() {
			//alert("hi")
			
			var div_img=$(this).find("img");
			if($("#btnRech").css("margin-right")=="587px")
		//if($(this).css("marginRight") == "704")
			{
				$('.reachUs').animate({"margin-right": '-=704px'},370);
				$('#btnRech').animate({"margin-right": '-=595px'},400,function(){
					div_img.attr("src","images/reachUp.png").parent().css({"margin-right":'+=8px'});
					
					});
				//alert("hello")
			}
			else
			{
				//alert("hi")
				$('.reachUs').animate({"margin-right": '+=704px'},370);
				$('#btnRech').animate({"margin-right": '+=595px'},400,function(){
						div_img.attr("src","images/reachUp_open.png").parent().css({"margin-right":'-=8px'});
						
					
					});
			}
  		});
	//});
// EndReach Us PopUp
	

	
});//End Document.ready


//Slider Code
function scrollToLeft() {
            var leftPos = $('#div_featured_icons').scrollLeft();
            $("#div_featured_icons").animate({ scrollLeft: leftPos - 60 }, 800);
        }
        function scrollToRight() {
            var leftPos = $('#div_featured_icons').scrollLeft();
            $("#div_featured_icons").animate({ scrollLeft: leftPos + 60 }, 800);
        }
        var m = 0;
        var n = 4000;
        var speed = 10;
        function scrollPics() {
            document.getElementById('div1').style.left = m + 'px';
            document.getElementById('div2').style.left = n + 'px';
            m--;
            n--;
            if (m == -4000) {
                m = 4000;
            }
            if (n == -4000) {
                n = 4000;
            }
            setTimeout('scrollPics()', speed);
      }
    window.onload = function () {
   scrollPics();
}
//End Slider Code

