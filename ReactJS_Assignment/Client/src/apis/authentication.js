export async function login(loginModel) {
  let responseModel = undefined;

  await axios.post('https://localhost:7069/authentication/login', loginModel)
    .then(result => {
      responseModel = result.data;

      // Set token to local storage here
    })
    .catch(error => {
      console.log(error.data);
    })

  return responseModel;
}