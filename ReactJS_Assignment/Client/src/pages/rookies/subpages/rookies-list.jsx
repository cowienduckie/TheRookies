import { Heading } from '@chakra-ui/react';
import { useLoaderData } from 'react-router-dom';
import { getRookies } from '../../../apis/rookies';
import { SimpleTable } from '../../../components/table/SimpleTable';
import { ToolBar } from '../../../components/tool-bar/ToolBar';

export async function loader() {
  const data = await getRookies();

  return data;
}

export function RookiesList() {
  const rookiesData = useLoaderData();

  const sortOrders = [
    { value: 'asc', text: 'Ascending' },
    { value: 'desc', text: 'Descending' }
  ];

  const sortOptions = [
    { value: 'name', text: 'Name' },
    { value: 'dob', text: 'Date of Birth' }
  ];

  const headers = ['Name', 'Gender', 'Date of Birth', 'Birth Place'];
  const fields = ['fullName', 'gender', 'dateOfBirth', 'birthPlace'];

  const noContent = (
    <Heading size='lg' textAlign='center'>NO CONTENT TO DISPLAY</Heading>
  );

  return <>
    <ToolBar
      sortOrders={sortOrders}
      sortOptions={sortOptions}
      minW='max-content'
      p={4} mb={5}
      alignItems='center'
      gap='6'
      bg="gray.50" />
      
    {rookiesData.length > 0
      ? <SimpleTable data={rookiesData} headers={headers} fields={fields} hasIndex hasAction />
      : noContent}
  </>
}