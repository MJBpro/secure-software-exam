import apiClient from '@/services/axios'

export async function post(user) {
  const response = await apiClient.post(`/user`, user );

  console.log("RES", response)
  if (response) {
    // console.log("Post Blocking",response)
    return response.data;
  }

  throw new Error('Unable to get user')
}
