import { AddIcon, SearchIcon } from "@chakra-ui/icons";
import { Flex, Input } from "@chakra-ui/react";
import { useState } from "react";
import { DEFAULT_SORT_FIELD, DEFAULT_SORT_ORDER } from "../../Constants/SystemConstants";
import { LinkButton } from "../LinkButton";
import { LinkIconButton } from "../LinkButton/LinkIconButton";
import { SortMenu } from "./SortMenu";

export function ToolBar(props) {
  const { createPath, sortOptions, sortOrders, searchPlaceholder, handleChange, queryState , hasSearchBar = false, ...otherProps } = props;

  const [query, setQuery] = useState(
  {
    sortOrder: DEFAULT_SORT_ORDER,
    sortField: DEFAULT_SORT_FIELD
  })

  return (
    <Flex {...otherProps}>
      <LinkButton
        path={createPath}
        px={10}
        label="Create"
        variant="outline"
        colorScheme="green"
        icon={AddIcon}
      />
      {hasSearchBar &&
        <>
          <Input
            w="50%"
            variant="filled"
            placeholder={searchPlaceholder || "Insert keywords to search here"}
          />
          <LinkIconButton
            path={``}
            label="Search"
            colorScheme="blue"
            icon={SearchIcon}
          />
        </>
      }
      <SortMenu
        sortOrders={sortOrders}
        sortOptions={sortOptions}
        defaultOrder={"asc"}
        closeOnSelect={false}
        buttonText="Sort By"
        handleChange={handleChange}
        queryState={queryState}
        buttonProps={{ colorScheme: "blue", variant: "outline", p: 5 }}
      />
    </Flex>
  )
}