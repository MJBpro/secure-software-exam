<!-- src/pages/callback-page.vue -->
<template>
  <div>
    Redirecting...
  </div>
</template>

<script>
import { onMounted } from 'vue';
import { useAuth0 } from '@auth0/auth0-vue';
import { useRouter } from 'vue-router';

export default {
  name: 'CallbackPage',
  setup() {
    const { isAuthenticated, getAccessTokenSilently } = useAuth0();
    const router = useRouter();

    onMounted(async () => {
      if (isAuthenticated.value) {
        try {
          const token = await getAccessTokenSilently();
          localStorage.setItem('access_token', token);
          router.push('/');
        } catch (error) {
          console.error('Error getting access token:', error);
        }
      }
    });
  }
};
</script>
