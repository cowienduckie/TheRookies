import axios from "axios";
import { BASE_URL, TOKEN_KEY } from "../constants/system-constants";

export async function getRookies() {
  const url = `${BASE_URL}/rookies`
  const response = await callApi('get', url);

  return response;
}

export async function getRookieById(id) {
  const url = `${BASE_URL}/rookies/${id}`;
  const response = await callApi('get', url);

  return response;
}

export async function createNewRookie(createModel) {
  const url = `${BASE_URL}/rookies`;
  const response = await callApi('post', url, createModel);

  return response;
}

export async function updateRookie(id, updateModel) {
  const url = `${BASE_URL}/rookies/${id}`;
  const response = await callApi('put', url, updateModel);

  return response;
}

export async function deleteRookie(id) {
  const url = `${BASE_URL}/rookies/${id}`;
  await callApi('delete', url);
}

export async function getProfile() {
  const url = `${BASE_URL}/users/profile`;
  const response = await callApi('get', url);

  return response;
}

export async function callApi(method, url, data = null) {
  let response = undefined;

  await axios({
    method: method,
    url: url,
    headers: { "Authorization": localStorage.getItem(TOKEN_KEY) },
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