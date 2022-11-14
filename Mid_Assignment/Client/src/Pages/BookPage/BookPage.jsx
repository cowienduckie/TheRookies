import { Box, Divider, Heading } from "@chakra-ui/react";
import { Outlet, redirect } from "react-router-dom";
import { ROLE_KEY } from "../../Constants/SystemConstants";

export function loader() {
  const role = localStorage.getItem(ROLE_KEY);

  if (role === null || role === "") {
    return redirect("/authenticate");
  }
}

export function BookPage() {
  return (
    <Box p={10}>
      <Heading size="xl">BOOK PAGE</Heading>
      <Divider my={7} />
      <Outlet />
    </Box>
  );
}
