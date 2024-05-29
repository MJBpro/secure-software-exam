const CallbackPage = () => import("@/pages/callback-page.vue");
import { createRouter, createWebHistory } from "vue-router";


const routes = [

    {
        path: "/callback",
        name: "callback",
        component: CallbackPage,
    }
];


const router= createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
})

export default router;
