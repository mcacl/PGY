//导航区
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
        imgs: [
            {path: 'GuunDoong/gd1.png', link: '#'},
            {path: 'GuunDoong/gd1.png', link: '#'},
            {path: 'GuunDoong/gd1.png', link: '#'}
        ]
    }
    // computed: {
    //     spimgp: function () {
    //         return this.$data.spimgpath;
    //     }
    // }
});
//页脚区
var vuefoot = new Vue({
    el: '#regfoot'
});