import axios from "axios";
import { BASE_URL, TOKEN_KEY } from "../Constants/SystemConstants";

export async function logIn(loginInfo) {  
  const url = `${BASE_URL}/api/authentication`;

  let response = undefined;

  await axios.post(url, loginInfo)
    .then(result => {
      response = result.data;

      localStorage.setItem(TOKEN_KEY, response.token);
    })
    .catch(error => {
      throw new Response("", {
        status: error.response.status,
        statusText: error.message
      })
    });

  return response;
}