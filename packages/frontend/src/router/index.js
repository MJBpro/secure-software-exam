// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../pages/HomePage.vue';
import LoginPage from '../pages/LoginPage.vue';
import UserProfilePage from '../pages/UserProfilePage.vue';
import AdminPage from '../pages/AdminPage.vue';
import NoAccessPage from '../pages/NoAccessPage.vue';
import { useAuth0 } from '@auth0/auth0-vue';

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
    },
    { 
      path: "/profile",
      component: UserProfilePage,
      name: "Profile",
      meta: {
        rolesRequired: ["Admin", "Member"]
      }
    },
    { 
      path: "/admin",
      component: AdminPage,
      name: "Admin",
      meta: {
        rolesRequired: ["Admin"]
      }
    },
    { 
      path: "/no-access",
      component: NoAccessPage,
      name: "NoAccess"
    }
  ]
});

router.beforeEach(async (to, from, next) => {
  const { isAuthenticated, user } = useAuth0();

  if (!isAuthenticated.value && to.name !== 'Login') {
    // redirect the user to the login page
    return next({ name: 'Login' });
  }

  if (isAuthenticated.value && to.name === 'Login') {
    return next({ name: 'HomePage' });
  }

  // Check user roles before continuing
  const rolesRequired = to.meta?.rolesRequired;
  if (rolesRequired && rolesRequired.length > 0) {
    const userRoles = user.value["http://localhost:8082/roles"];
    const hasRequiredRole = rolesRequired.some(role => userRoles.includes(role));
    
    if (!hasRequiredRole) {
      return next({ name: 'HomePage' });
    }
  }

  next();
});

export default router;
