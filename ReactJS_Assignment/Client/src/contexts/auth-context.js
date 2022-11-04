import { createContext } from "react";
import { TOKEN_KEY } from "../constants/system-constants";

export const authContext = createContext({
  authenticated: localStorage.getItem(TOKEN_KEY) != null,
  setAuthenticated: (auth) => {}
});