import {
  Table,
  TableContainer,
  Tbody,
  Thead,
  Tr,
  Th,
  Td,
  Text,
} from "@chakra-ui/react";
import { TableAction } from "./TableAction";

export function SimpleTable(props) {
  const {
    data = [],
    headers = [],
    fields = [],
    hasIndex = true,
    hasAction = false,
    resourcePath,
    hasDetailView,
    ...otherProps
  } = props;

  return (
    <TableContainer>
      <Table variant="simple">
        <Thead>
          <Tr>
            {hasIndex && <Th>#</Th>}
            {headers && headers.map((header) => <Th key={header}>{header}</Th>)}
            {hasAction && <Th>Actions</Th>}
          </Tr>
        </Thead>
        <Tbody>
          {data.map((value, index) => (
            <Tr key={index}>
              {hasIndex && <Td>{index + 1}</Td>}
              {fields.map((fieldName) => (
                <Td key={`${index}-${fieldName}`} overflow="auto">
                  <Text maxW="sm">{value[fieldName]}</Text>
                </Td>
              ))}
              {hasAction && (
                <Td>
                  <TableAction
                    objectId={value.id}
                    resourcePath={resourcePath}
                    hasDetailView={hasDetailView}
                  />
                </Td>
              )}
            </Tr>
          ))}
        </Tbody>
      </Table>
    </TableContainer>
  );
}
