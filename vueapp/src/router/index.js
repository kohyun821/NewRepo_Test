import { createRouter, createWebHistory } from 'vue-router'
//import HomeView from '../views/HomeView.vue'

const routes = [
    {
        path: '/',
        name: 'home',
        // component: HomeView
        component: () => import('../views/DepManageView.vue')
    },
    {
        path: '/manage/dep',
        name: 'DepManage',
        component: () => import('../views/DepManageView.vue')
    },
    {
        path: '/manage/emp',
        name: 'EmpManage',
        component: () => import('../views/EmpManageView.vue')
    },
    {
        path: '/manage/mtr',
        name: 'MtrManage',
        component: () => import('../views/MtrManageView.vue')
    },
    {
        path: '/use/mtr',
        name: 'MtrUse',
        component: () => import('../views/MtrUseView.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
