import { Heading } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useLoaderData, useNavigate } from "react-router-dom";
import { getBooks } from "../../../Apis/BookApis";
import { SimpleTable, ToolBar } from "../../../Components";
import { Pagination } from "../../../Components/Table/Pagination";
import { sortOrders } from "../../../Constants/FilterOptions";
import {
  DEFAULT_PAGE_INDEX,
  DEFAULT_PAGE_SIZE,
  DEFAULT_SORT_FIELD,
  DEFAULT_SORT_ORDER,
} from "../../../Constants/SystemConstants";
import { queryToString } from "../../../Helpers/ApiHelper";

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
  const wrapper = await getBooks(queriesText);

  return { wrapper, queryFromUrl };
}

export function ViewBookListPage() {
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
    { value: "1", text: "Name" },
  ];

  const headers = ["Id", "Name", "Description"];
  const fields = ["id", "name", "description"];

  const noContent = (
    <Heading size="lg" textAlign="center">
      NO CONTENT TO DISPLAY
    </Heading>
  );

  const table = (
    <>
      <SimpleTable
        data={wrapper.data}
        headers={headers}
        fields={fields}
        resourcePath={"/admin/books"}
        hasIndex
        hasAction
        hasDetailView
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
        createPath="/admin/books/new"
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
