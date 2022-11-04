import axios from "axios";
import { BASE_URL, TOKEN_KEY } from "../constants/system-constants";

export async function login(loginModel) {
  let responseModel = undefined;

  await axios.post(`${BASE_URL}/authentication/login`, loginModel)
    .then(result => {
      responseModel = result.data;

      localStorage.setItem(TOKEN_KEY, responseModel.token);
    })
    .catch(error => {
      console.log(error);
    })

  return responseModel;
}