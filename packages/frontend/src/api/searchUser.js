import apiClient from '@/services/axios'

export async function get(search) {
  const response = await apiClient.get(`/user/search`, {params: {searchTerm: search}} );

  console.log("RES", response)
  if (response) {
    // console.log("Post Blocking",response)
    return response.data;
  }

  throw new Error('Unable to find user')
}
