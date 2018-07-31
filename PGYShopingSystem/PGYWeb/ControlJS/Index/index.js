var Commondata = {
    title: '蒲公英',
    navs: [
        {name: '导航1', link: '#'},
        {name: '关于我们', link: '#'},
        {name: '导航3', link: '#'}]
};
new Vue({el: '#navtitle', data: Commondata});
new Vue({el: 'a', data: Commondata});
new Vue({
    el: 'li', methods: {
        addActive($event) {
            $event.currentTarget.className = "active";
        },
        removeActive($event) {
            $event.currentTarget.className = "";
        }
    }
});