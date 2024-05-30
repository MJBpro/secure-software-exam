import apiClient from '@/services/axios'

export async function del(id) {
  const response = await apiClient.delete(`/user/${id}` );

  console.log("RES", response)
  if (response) {
    // console.log("Post Blocking",response)
    return response.data;
  }

  throw new Error('Unable to get user')
}
