import { AddIcon, SearchIcon } from "@chakra-ui/icons";
import { Flex, Input } from "@chakra-ui/react";
import { LinkButton } from "../link-button/LinkButton";
import { LinkIconButton } from "../link-button/LinkIconButton";
import { SortMenu } from "../sort-menu/SortMenu";

export function ToolBar(props) {
  const { createPath, sortOptions, sortOrders, ...otherProps } = props;

  return (
    <Flex {...otherProps}>
      <LinkButton
        path={createPath}
        px={10}
        label='Create'
        variant='outline'
        colorScheme='green'
        icon={AddIcon} />
      <Input
        w='50%'
        variant='filled'
        placeholder='Search by id, full name' />
      <LinkIconButton
        path={``}
        label='Search'
        colorScheme='blue'
        icon={SearchIcon} />
      <SortMenu
        sortOrders={sortOrders}
        sortOptions={sortOptions}
        defaultOrder={'asc'}
        closeOnSelect={false}
        buttonText='Sort By'
        buttonProps={{ colorScheme: 'blue', variant: 'outline', p: 5 }} />
    </Flex>
  )
}