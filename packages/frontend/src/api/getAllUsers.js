import apiClient from '@/services/axios'

export async function get(id) {
  const response = await apiClient.get(`/user/${id}` );

  console.log("RES", response)
  if (response) {
    // console.log("Post Blocking",response)
    return response.data;
  }

  throw new Error('Unable to get user')
}
