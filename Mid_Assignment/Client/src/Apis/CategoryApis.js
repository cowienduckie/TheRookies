import { BASE_URL } from "../Constants/SystemConstants";
import { callApi } from "../Helpers/ApiHelper";

const url = `${BASE_URL}/api/categories`;

export async function getCategories(queries = '') {
  return await callApi('get', url + queries);
}

export async function getCategoryById(id) {
  return await callApi('get', url + '/' + id);
}

export async function createCategory(createModel) {
  return await callApi('post', url, createModel);
}

export async function updateCategory(updateModel) {
  return await callApi('put', url, updateModel);
}

export async function deleteCategory(id) {
  return await callApi('delete', url + '/' + id);
}