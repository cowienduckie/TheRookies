import { Heading } from "@chakra-ui/react";
import { useLoaderData } from "react-router-dom";
import { getCategories } from "../../../Apis/CategoryApis";
import { SimpleTable, ToolBar } from "../../../Components";
import { Pagination } from "../../../Components/Table/Pagination";

export async function loader() {
  return await getCategories();
}

export function ViewCategoryListPage() {
  const wrapper = useLoaderData();

  const sortOrders = [
    { value: 'asc', text: 'Ascending' },
    { value: 'desc', text: 'Descending' }
  ];
  const sortOptions = [
    { value: 'id', text: 'Id' },
    { value: 'name', text: 'Name' },
  ];

  const headers = ['Id', 'Name'];
  const fields = ['id', 'name'];

  const noContent = (
    <Heading size='lg' textAlign='center'>NO CONTENT TO DISPLAY</Heading>
  );

  const table = (
    <>
      <SimpleTable
        data={wrapper.data}
        headers={headers}
        fields={fields}
        resourcePath={"/admin/categories"}
        hasIndex
        hasAction
        hasDetailView={false}
      />
      <Pagination
        my={10}
        pageIndex={wrapper.pageIndex} 
        pageSize={wrapper.pageSize} 
        totalPage={wrapper.totalPage} 
        totalRecord={wrapper.totalRecord} 
        hasPreviousPage={wrapper.hasPreviousPage} 
        hasNextPage={wrapper.hasNextPage}
        colorScheme={"blue"} />
    </>
  )

  return <>
    <ToolBar
      createPath='/admin/categories/new'
      sortOrders={sortOrders}
      sortOptions={sortOptions}
      minW='max-content'
      p={4} mb={5}
      alignItems='center'
      gap='6'
      bg="gray.50" />

    {(wrapper && wrapper.data.length > 0)
      ? table
      : noContent}
  </>
}