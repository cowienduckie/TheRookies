import { MenuButton, Button, Menu, MenuList, MenuOptionGroup, MenuItemOption, MenuDivider } from "@chakra-ui/react";
import { useState } from "react";

export function SortMenu(props) {
  const {
    sortOrders = [],
    sortOptions = [],
    defaultOrder,
    defaultOption,
    closeOnSelect = true,
    buttonText,
    buttonProps = {},
    handleChange,
    queryState,
    ...otherProps } = props;

  const [query, setQuery] = useState(queryState)

  return (
    <Menu closeOnSelect={closeOnSelect}>
      <MenuButton as={Button} {...buttonProps}>
        {buttonText}
      </MenuButton>
      <MenuList>
        <MenuOptionGroup
          value={query.sortOrder}
          title='Order'
          type='radio'
          onChange={(value) => {
            setQuery({...query, ['sortOrder']: value});
            handleChange({...query, ['sortOrder']: value});
          }}
        >
          {sortOrders.map(order =>
            <MenuItemOption
              key={order.value}
              value={order.value}
            >
              {order.text}
            </MenuItemOption>
          )}
        </MenuOptionGroup>
        <MenuDivider />
        <MenuOptionGroup
          value={query.sortField}
          title='Field'
          type='radio'
          onChange={(value) => {
            setQuery({...query, ['sortField']: value})
            handleChange({...query, ['sortField']: value});
          }}
        >
          <MenuItemOption value={""}>(none)</MenuItemOption>
          {sortOptions.map(option =>
            <MenuItemOption
              key={option.value}
              value={option.value}
            >
              {option.text}
            </MenuItemOption>
          )}
        </MenuOptionGroup>
      </MenuList>
    </Menu>
  )
}