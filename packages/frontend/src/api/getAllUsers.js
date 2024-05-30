import apiClient from '@/services/axios'

export async function get() {
  const response = await apiClient.get(`/user/all` );

  console.log("RES", response)
  if (response) {
    // console.log("Post Blocking",response)
    return response.data;
  }

  throw new Error('Unable to get user')
}
