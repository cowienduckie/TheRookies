import { Heading } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useLoaderData, useNavigate } from "react-router-dom";
import { getBorrowRequests } from "../../Apis/BorrowRequestApis";
import { ToolBar } from "../../Components";
import { BorrowRequestTable } from "../../Components/Table/BorrowRequestTable";
import { Pagination } from "../../Components/Table/Pagination";
import { sortOrders } from "../../Constants/FilterOptions";
import {
  DEFAULT_PAGE_INDEX,
  DEFAULT_PAGE_SIZE,
  DEFAULT_SORT_FIELD,
  DEFAULT_SORT_ORDER,
} from "../../Constants/SystemConstants";
import { queryToString } from "../../Helpers/ApiHelper";

export async function loader({ request }) {
  const url = new URL(request.url);
  const pageIndex = url.searchParams.get("pageIndex") ?? DEFAULT_PAGE_INDEX;
  const pageSize = url.searchParams.get("pageSize") ?? DEFAULT_PAGE_SIZE;
  const sortOrder = url.searchParams.get("sortOrder") ?? DEFAULT_SORT_ORDER;
  const sortField = url.searchParams.get("sortField") ?? DEFAULT_SORT_FIELD;

  const queryFromUrl = {
    pageIndex,
    pageSize,
    sortOrder,
    sortField,
  };

  const queriesText = queryToString(queryFromUrl);

  const wrapper = await getBorrowRequests(queriesText);

  return { wrapper, queryFromUrl };
}

export function BorrowBookListPage() {
  const { wrapper, queryFromUrl } = useLoaderData();
  const navigate = useNavigate();

  const [query, setQuery] = useState({
    pageIndex: wrapper.pageIndex,
    pageSize: wrapper.pageSize,
    sortOrder: queryFromUrl.sortOrder,
    sortField: queryFromUrl.sortField,
  });

  const handleQueryChange = (newState) => {
    setQuery(newState);
  };

  useEffect(() => {
    const queriesText = queryToString(query);

    navigate(queriesText);
  }, [query]);

  const sortOptions = [
    { value: "0", text: "Id" },
    { value: "6", text: "Status" },
    { value: "2", text: "Requested At" },
    { value: "4", text: "Approved At" },
    { value: "5", text: "Approved By" },
  ];

  const noContent = (
    <Heading size="lg" textAlign="center">
      NO CONTENT TO DISPLAY
    </Heading>
  );

  const table = (
    <>
      <BorrowRequestTable
        data={wrapper.data}
        resourcePath={"/borrow-books"}
        hasIndex
        hasAction
        isAdmin={false}
      />
      <Pagination
        my={10}
        queryState={query}
        handleChange={handleQueryChange}
        pageIndex={wrapper.pageIndex}
        pageSize={wrapper.pageSize}
        totalPage={wrapper.totalPage}
        totalRecord={wrapper.totalRecord}
        hasPreviousPage={wrapper.hasPreviousPage}
        hasNextPage={wrapper.hasNextPage}
        colorScheme={"blue"}
      />
    </>
  );

  return (
    <>
      <ToolBar
        handleChange={handleQueryChange}
        queryState={query}
        hasCreate={false}
        sortOrders={sortOrders}
        sortOptions={sortOptions}
        minW="max-content"
        p={4}
        mb={5}
        alignItems="center"
        gap="6"
        bg="gray.50"
      />

      {wrapper && wrapper.data.length > 0 ? table : noContent}
    </>
  );
}
