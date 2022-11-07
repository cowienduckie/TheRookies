export async function callApi(method, url, data = null) {
  let response = undefined;

  await axios({
    method: method,
    url: url,
    headers: {"Authorization": localStorage.getItem(TOKEN_KEY)},
    data: data
  })
    .then(result => {
      response = result.data
    })
    .catch(error => {
      console.log(error)
    })

  return response;
}