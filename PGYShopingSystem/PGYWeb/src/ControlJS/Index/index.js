/*//导航区
var vuetitle = new Vue({
    el: '#regtitle',
    data: {
        navs: [
            {name: '导航1', link: '#'},
            {name: '关于我们', link: '#'},
            {name: '导航3', link: '#'}]
    },
    methods: {
        addActive: function (event) {
            event.currentTarget.className = "active";
        },
        removeActive: function (event) {
            event.currentTarget.className = "";
        }
    }
});
//主窗口区
var vuemain = new Vue({
    el: '#regmain',
    data: {
        imgsgd: [
            {path: 'GuunDoong/gd1.png', link: '#', alt: '1'},
            {path: 'GuunDoong/gd1.png', link: '#', alt: '2'},
            {path: 'GuunDoong/gd1.png', link: '#', alt: '3'}
        ],
        imgstj: [
            {path: 'TuuiJiian/pg.png', link: '#', alt: '1'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '2'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '3'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '4'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '5'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '6'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '7'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '8'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '9'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '10'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '11'},
            {path: 'TuuiJiian/pg.png', link: '#', alt: '12'}
        ]
    },
    computed: {
        imgstjgroup: function () {
            var imggroup = [];
            var imgs = this.imgstj;
            var num = 6;//每组5个
            for (let i = 0; i < imgs.length; i++) {
                var index = parseInt(i / num);
                if (imggroup.length < num && index >= imggroup.length) {
                    imggroup.push([]);
                }
                imggroup[index].push(imgs[i]);
            }
            return imggroup;
        }
    }
});
//页脚区
var vuefoot = new Vue({
    el: '#regfoot'
});*/



var myComponent = Vue.extend({
    template: '<p>组件</p>'
});
// 2.注册组件，并指定组件的标签，组件的HTML标签为<my-component>
Vue.component('indexcp', myComponent)

new Vue({
    el: '#app'
});