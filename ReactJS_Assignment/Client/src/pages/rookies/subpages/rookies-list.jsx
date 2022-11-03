import { ViewIcon, EditIcon, DeleteIcon, AddIcon, SearchIcon } from '@chakra-ui/icons';
import {
  Button, ButtonGroup, IconButton,
  TableContainer, Table, Thead, Tbody, Th, Td, Tr,
  Menu, MenuOptionGroup, MenuButton, MenuList, MenuDivider, MenuItemOption,
  Flex, Heading, Input, LinkBox
} from '@chakra-ui/react';
import { useLoaderData, NavLink, Form } from 'react-router-dom';
import { getRookies } from '../../../apis/rookies';
import { SimpleTable } from '../../../components/table/SimpleTable';

export async function loader() {
  const data = await getRookies();

  return data;
}

export function RookiesList() {
  const rookiesData = useLoaderData();

  const noContent = (
    <Heading size='lg' textAlign='center'>NO CONTENT TO DISPLAY</Heading>
  );
  
  const headers = ['Name', 'Gender', 'Date of Birth', 'Birth Place'];
  const fields = ['fullName', 'gender', 'dateOfBirth', 'birthPlace'];

  return <>
    <Flex
      minW='max-content'
      p={4}
      mb={5}
      alignItems='center'
      gap='6'
      bg="gray.50">
      <Button
        px={10}
        aria-label='Create'
        variant='outline'
        colorScheme='green'>
        <AddIcon />
      </Button>
      <Input
        w='50%'
        variant='filled'
        placeholder='Search by id, full name' />
      <IconButton
        aria-label='Search'
        colorScheme='blue'
        icon={<SearchIcon />} />
      <Menu closeOnSelect={false}>
        <MenuButton as={Button} colorScheme='blue' variant='outline' p={5}>
          Sort By
        </MenuButton>
        <MenuList>
          <MenuOptionGroup defaultValue='asc' title='Order' type='radio'>
            <MenuItemOption value='asc'>Ascending</MenuItemOption>
            <MenuItemOption value='desc'>Descending</MenuItemOption>
          </MenuOptionGroup>
          <MenuDivider />
          <MenuOptionGroup title='Field' type='radio'>
            <MenuItemOption value=''>(none)</MenuItemOption>
            <MenuItemOption value='name'>Name</MenuItemOption>
            <MenuItemOption value='dob'>Date of Birth</MenuItemOption>
          </MenuOptionGroup>
        </MenuList>
      </Menu>
    </Flex>

    {rookiesData.length > 0
      ? <SimpleTable data={data} headers={headers} fields={fields} hasIndex hasAction /> 
      : noContent}
  </>
}