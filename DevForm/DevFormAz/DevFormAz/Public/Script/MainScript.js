
//for slider sec Index
$(document).ready(function () {
    document.body.style.overflow = "none";
    $("#devformcontent").css({ "display": "none" });
    setTimeout(function () {
        $('.preloader').fadeOut('slow');
        document.body.style.overflowY = "scroll";
        $("#devformcontent").css({ "display": "block" })

    }, 500)

    setTimeout(function () {
        $(".mainSlider h1").addClass("animated fadeInDown");

    }, 500)
    setTimeout(function () {
        $(".mainSlider p").addClass("animated fadeIn");
        $(".mainSlider hr").addClass("animated fadeIn");
        $(".mainSlider a").addClass("animated fadeIn");
    }, 500)
});
// When scroll header is fixed
$(document).scroll(function(){
    $("header").css({"opacity":0});
    if(window.pageYOffset >= 103){
        $("header").addClass("headerFixed");
        $("header").css({"opacity":1});

    }else if(window.pageYOffset <= 1){
        $("header").removeClass("headerFixed");
        $("header").css({"opacity":1});
    }

})







//For navbar

$("#resNavMenu").click(function(){
    $(".resNavLayout").css({
        "display":"block"
    });
    document.body.style.overflow = "hidden";
})

$(".closeNav").click(function(){
    $(".resNavLayout").css({
        "display":"none"
    });
    document.body.style.overflowY = "scroll";
})


//Particle

//Indexpage particle
$("#particle.particleMain").particleground({
    dotColor: '#ffffff',
    lineColor: '#ffffff'
});

//login register page particle
$('#particle').particleground({
    dotColor: '#000000',
    lineColor: '#101820ff'
});

// end particle

  

// for share post tags input
$.each(document.getElementsByClassName("tags-input"),function(index,el){
    let hiddenInput = document.createElement('input'),
        mainInput = document.createElement('input'),
        tags = [];
    
    hiddenInput.setAttribute('type', 'hidden');
    hiddenInput.setAttribute('name', el.getAttribute('data-name'));

    mainInput.setAttribute('type', 'text');
    mainInput.classList.add('main-input');
    mainInput.addEventListener('keyup', function (e) {
        if(e.keyCode === 32){
            let enteredTags = mainInput.value.split(' ');
            if (enteredTags.length > 1) {
                enteredTags.forEach(function (t) {
                    let filteredTag = filterTag(t);
                    if (filteredTag.length > 0)
                        addTag(filteredTag);
                });
                mainInput.value = '';
            }
        }
        
    });

    mainInput.addEventListener('keydown', function (e) {
        let keyCode = e.which || e.keyCode;
        if (keyCode === 8 && mainInput.value.length === 0 && tags.length > 0) {
            removeTag(tags.length - 1);
        }
    });

    el.appendChild(mainInput);
    el.appendChild(hiddenInput);


    function addTag (text) {
        let tag = {
            text: text,
            element: document.createElement('span'),
        };

        tag.element.classList.add('tag');
        tag.element.textContent = tag.text;

        let closeBtn = document.createElement('span');
        closeBtn.classList.add('close');
        closeBtn.addEventListener('click', function () {
            removeTag(tags.indexOf(tag));
        });
        tag.element.appendChild(closeBtn);

        tags.push(tag);

        el.insertBefore(tag.element, mainInput);

        refreshTags();
    }

    function removeTag (index) {
        let tag = tags[index];
        tags.splice(index, 1);
        el.removeChild(tag.element);
        refreshTags();
    }

    function refreshTags () {
        let tagsList = [];
        tags.forEach(function (t) {
            tagsList.push(t.text);
        });
        hiddenInput.value = tagsList.join(',');
    }

    function filterTag (tag) {
        return tag.replace(/[^\w #]/g, '').trim().replace(/\W+/g, '#');
    }
});



//Profile page data tabs
const targetTabs = document.querySelectorAll('[data-tab-target]');
const contentTabs = document.querySelectorAll('[data-tab-content]');

targetTabs.forEach(tabs=>{
    tabs.addEventListener('click',function(){
        const target = document.querySelector(tabs.dataset.tabTarget)
        contentTabs.forEach(contentTab=>{
            contentTab.classList.remove('active');
        });
        targetTabs.forEach(tab=>{
            tab.classList.remove('targetActive');
        });

        tabs.classList.add('targetActive');
        target.classList.add('active');
    })
});
