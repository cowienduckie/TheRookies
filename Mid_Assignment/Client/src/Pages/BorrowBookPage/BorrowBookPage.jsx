import { Box, Divider, Heading } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";
import { ROLE_KEY, SUPER_USER } from "../../Constants/SystemConstants";

export function loader() {
  const role = localStorage.getItem(ROLE_KEY);

  if (role === null || role === "") {
    return redirect("/authenticate");
  }

  if (role === SUPER_USER) {
    throw new Response("", {
      status: 401,
      statusText: "UNAUTHORIZED",
    });
  }
}

export function BorrowBookPage() {
  return (
    <Box p={10}>
      <Heading size="xl">BORROW BOOK PAGE</Heading>
      <Divider my={7} />
      <Outlet />
    </Box>
  );
}
