import ApartmentCard from "../ApartmentCard/ApartmentCard";
import ApartmentCardSkeleton from "../ApartmentCard/ApartmentCardSkeleton";
import type ApartmentGridProps from "./ApartmentGridProps";

export default function ApartmentGrid({
  apartments,
  isLoading = false,
  error = null,
  onRetry,
}: ApartmentGridProps) {
  if (isLoading) {
    return (
      <div
        role="status"
        aria-label="Loading apartments"
        className="grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4"
      >
        {Array.from({ length: 8 }).map((_, index) => (
          <ApartmentCardSkeleton key={index} />
        ))}
      </div>
    );
  }

  if (error) {
    return (
      <div
        role="alert"
        className="flex min-h-64 flex-col items-center justify-center px-4 text-center"
      >
        <div className="flex size-12 items-center justify-center rounded-full bg-red-50">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth={1.5}
            stroke="red"
            className="size-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126ZM12 15.75h.007v.008H12v-.008Z"
            />
          </svg>
        </div>

        <h2 className="mt-4 text-lg font-semibold">Something went wrong</h2>

        <p className="mt-1 max-w-sm text-sm text-gray-500">{error}</p>

        {onRetry && (
          <button
            type="button"
            onClick={onRetry}
            className="mt-5 rounded-xl bg-black px-5 py-2.5 text-sm font-medium text-white transition-colors hover:bg-gray-800 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-black"
          >
            Retry
          </button>
        )}
      </div>
    );
  }

  if (apartments.length === 0) {
    return (
      <div className="flex min-h-64 flex-col items-center justify-center px-4 text-center">
        <div className="flex size-12 items-center justify-center rounded-full bg-gray-100">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
            fill="currentColor"
            className="size-5"
          >
            <path
              fillRule="evenodd"
              d="M9 3.5a5.5 5.5 0 1 0 0 11 5.5 5.5 0 0 0 0-11ZM2 9a7 7 0 1 1 12.452 4.391l3.328 3.329a.75.75 0 1 1-1.06 1.06l-3.329-3.328A7 7 0 0 1 2 9Z"
              clipRule="evenodd"
            />
          </svg>
        </div>

        <h2 className="mt-4 text-lg font-semibold">No apartments found</h2>

        <p className="mt-1 max-w-sm text-sm text-gray-500">
          Try changing your destination, date range, number of guests, or
          filters.
        </p>
      </div>
    );
  }

  return (
    <div className="grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
      {apartments.map((apartment) => (
        <ApartmentCard key={apartment.id} {...apartment} />
      ))}
    </div>
  );
}
