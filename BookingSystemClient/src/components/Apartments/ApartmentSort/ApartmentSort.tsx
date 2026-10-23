import type ApartmentSortOption from "../../../types/ApartmentSortOption";
import type ApartmentSortProps from "./ApartmentSortProps";

export default function ApartmentSort({ value, onChange }: ApartmentSortProps) {
  return (
    <>
      <div className="flex items-center gap-2">
        <label
          htmlFor="apartment-sort"
          className="text-sm font-medium text-gray-700"
        >
          Sort by:
        </label>

        <select
          id="apartment-sort"
          value={value}
          onChange={(event) =>
            onChange(event.target.value as ApartmentSortOption)
          }
          className="rounded-xl border border-gray-300 bg-white px-3 py-2 text-sm focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-900"
        >
          <option value="newest">Newest first</option>
          <option value="oldest">Oldest first</option>
          <option value="price-asc">Price: Low to High</option>
          <option value="price-desc">Price: High to Low</option>
        </select>
      </div>
    </>
  );
}
