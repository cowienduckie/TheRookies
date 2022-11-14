import { Outlet, redirect } from "react-router-dom";
import { NORMAL_USER, ROLE_KEY } from "../../Constants/SystemConstants";

export function loader() {
  const role = localStorage.getItem(ROLE_KEY);

  if (role === null || role === "") {
    return redirect("/authenticate");
  }

  if (role === NORMAL_USER) {
    throw new Response("", {
      status: 401,
      statusText: "UNAUTHORIZED",
    });
  }
}

export function AdminPage() {
  return <Outlet />;
}
