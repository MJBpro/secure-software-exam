// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../pages/HomePage.vue';
import CallbackPage from '../pages/callback-page.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: '/callback', component: CallbackPage }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
