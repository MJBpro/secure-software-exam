// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../pages/HomePage.vue';
import LoginPage from '../pages/LoginPage.vue';

import {  useAuth0 } from '@auth0/auth0-vue';


const router = createRouter({
  history: createWebHistory(),
  routes: [
    { 
      path: "/",
      component: HomePage,
      name: "HomePage"
    },
    { 
      path: "/login",
      component: LoginPage,
      name: "Login"
    }

  ],
});

router.beforeEach((to) =>{
  
  if(!useAuth0().isAuthenticated.value && to.name !== 'Login'){
    // redirect the user to the login page
    return { name: 'Login' }
  }

  console.log(useAuth0().isAuthenticated.value, to.name)

  if((useAuth0().isAuthenticated.value && to.name == 'Login')){
    return { name: 'HomePage' }
  }

})



export default router;
