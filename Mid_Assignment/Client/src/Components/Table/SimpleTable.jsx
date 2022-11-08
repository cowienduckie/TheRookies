import { Table, TableContainer, Tbody, Thead, Tr, Th, Td } from "@chakra-ui/react";
import { TableAction } from "./TableAction";

export function SimpleTable(props) {
  const { data = [], headers = [], fields = [], hasIndex = true, hasAction = false, ...otherProps } = props;

  return (
    <TableContainer>
      <Table variant='simple' >
        <Thead>
          <Tr>
            {hasIndex && <Th>#</Th>}
            {headers && headers.map(header =>
              <Th key={header}>{header}</Th>
            )}
            {hasAction && <Th>Actions</Th>}
          </Tr>
        </Thead>
        <Tbody>
          {data.map((value, index) =>
            <Tr key={index}>
              {hasIndex && <Td>{index + 1}</Td>}
              {fields.map(fieldName =>
                <Td key={`${index}-${fieldName}`}>{value[fieldName]}</Td>
              )}
              {hasAction && <Td><TableAction objectId={value.id} /></Td>}
            </Tr>
          )}
        </Tbody>
      </Table>
    </TableContainer>
  )
}