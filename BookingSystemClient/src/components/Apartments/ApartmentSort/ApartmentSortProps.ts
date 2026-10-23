import type ApartmentSortOption from "../../../types/ApartmentSortOption";

export default interface ApartmentSortProps {
  value: ApartmentSortOption;
  onChange: (value: ApartmentSortOption) => void;
}
