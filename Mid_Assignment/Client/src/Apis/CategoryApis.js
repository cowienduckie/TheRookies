import { BASE_URL } from "../Constants/SystemConstants";
import { callApi } from "../Helpers/ApiHelper";

export async function getCategories() {
  const url = `${BASE_URL}/api/categories`;

  return await callApi('get', url);
}