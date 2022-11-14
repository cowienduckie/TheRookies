import { Flex, Link, Heading, Button, Spacer, LinkBox } from "@chakra-ui/react";
import { useContext } from "react";
import { NavLink } from "react-router-dom";
import { NORMAL_USER, SUPER_USER } from "../../Constants/SystemConstants";
import { AuthContext } from "../../Contexts/AuthContext";
import { BorrowRequestContext } from "../../Contexts/BorrowRequestContext";

export function Header(props) {
  const authContext = useContext(AuthContext);
  const requestContext = useContext(BorrowRequestContext);

  const onLogOut = () => {
    authContext.clearAuthInfo();
    requestContext.clearRequest();
  };

  return (
    <>
      <Flex
        minW="max-content"
        py={4}
        px={20}
        alignItems="center"
        gap="6"
        bg="gray.100"
      >
        <Heading size="md">BOOK LIBRARY</Heading>
        <Link as={NavLink} to="/">
          HOME
        </Link>
        {authContext.authenticated && (
          <Link as={NavLink} to="/books">
            BOOKS
          </Link>
        )}
        {authContext.userRole === NORMAL_USER && (
          <Link as={NavLink} to="/borrow-books">
            BORROW BOOKS
          </Link>
        )}
        {authContext.userRole === SUPER_USER && (
          <>
            <Link as={NavLink} to="/admin/categories">
              ADMIN CATEGORY
            </Link>
            <Link as={NavLink} to="/admin/books">
              ADMIN BOOK
            </Link>
            <Link as={NavLink} to="/admin/borrow-requests">
              ADMIN BORROW REQUEST
            </Link>
          </>
        )}
        <Spacer />
        {authContext.userRole === NORMAL_USER && (
          <Link as={NavLink} to="/borrow-books/new">
            REQUEST {`[${requestContext.borrowRequest.length}]`}
          </Link>
        )}
        {!authContext.authenticated ? (
          <LinkBox as={NavLink} to="/authenticate">
            <Button colorScheme="blue">LOG IN</Button>
          </LinkBox>
        ) : (
          <>
            <Link as={NavLink} to="/" onClick={onLogOut}>
              LOG OUT
            </Link>
          </>
        )}
      </Flex>
    </>
  );
}
