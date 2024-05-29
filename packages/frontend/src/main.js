// main.js or main.ts
import { createApp } from 'vue';
import App from './App.vue';
import { createAuth0 } from '@auth0/auth0-vue';

const app = createApp(App);

app.use(
  createAuth0({
    domain: "pbsw.eu.auth0.com",
    clientId: "B8Wl4W10gRdaldhiH03BDkfWl3EO9WAR",
    authorizationParams: {
      redirect_uri: window.location.origin,
    },
    cacheLocation: 'localstorage', // This is not recommended for sensitive data
  })
);

app.mount('#app');
