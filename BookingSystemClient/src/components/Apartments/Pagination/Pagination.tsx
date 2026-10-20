import type PaginationProps from "./PaginationProps";

function getVisiblePages(
  currentPage: number,
  totalPages: number,
): (number | string)[] {
  const pages = new Set<number>();

  pages.add(1);
  pages.add(totalPages);

  for (let page = currentPage - 1; page <= currentPage + 1; page++) {
    if (page >= 1 && page <= totalPages) {
      pages.add(page);
    }
  }

  const sortedPages = [...pages].sort((a, b) => a - b);
  const result: (number | string)[] = [];

  sortedPages.forEach((page, index) => {
    if (index > 0) {
      const previousPage = sortedPages[index - 1];
      const gap = page - previousPage;

      if (gap === 2) {
        result.push(previousPage + 1);
      } else if (gap > 2) {
        result.push("...");
      }
    }

    result.push(page);
  });

  return result;
}

export default function Pagination({
  currentPage,
  totalPages,
  onPageChange,
}: PaginationProps) {
  if (totalPages <= 1) return null;

  const visiblePages = getVisiblePages(currentPage, totalPages);

  return (
    <>
      <nav
        aria-label="Apartment pagination"
        className="mt-8 flex items-center justify-center gap-4"
      >
        <button
          type="button"
          disabled={currentPage <= 1}
          onClick={() => onPageChange(currentPage - 1)}
          aria-label="Previous page"
          className="flex items-center justify-between rounded-4xl border px-4 py-2 cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
            fill="currentColor"
            className="size-5"
          >
            <path
              fillRule="evenodd"
              d="M11.78 5.22a.75.75 0 0 1 0 1.06L8.06 10l3.72 3.72a.75.75 0 1 1-1.06 1.06l-4.25-4.25a.75.75 0 0 1 0-1.06l4.25-4.25a.75.75 0 0 1 1.06 0Z"
              clipRule="evenodd"
            />
          </svg>
          Previous
        </button>

        <div className="hidden items-center gap-2 sm:flex">
          {visiblePages.map((page, index) =>
            typeof page === "number" ? (
              <button
                key={page}
                type="button"
                onClick={() => onPageChange(page)}
                aria-label={`Go to page ${page}`}
                aria-current={currentPage === page ? "page" : undefined}
                className={`rounded-4xl border px-4 py-2 transition-colors ${
                  currentPage === page
                    ? "border-gray-900 bg-gray-900 text-white"
                    : "border-gray-300 hover:bg-gray-100"
                }`}
              >
                {page}
              </button>
            ) : (
              <span key={`ellipsis-${index}`} className="px-1">
                ...
              </span>
            ),
          )}
        </div>

        <span className="text-sm text-gray-600 sm:hidden">
          Page {currentPage} of {totalPages}
        </span>

        <button
          type="button"
          disabled={currentPage >= totalPages}
          onClick={() => onPageChange(currentPage + 1)}
          aria-label="Next page"
          className="flex items-center justify-between rounded-4xl border px-4 py-2 cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed"
        >
          Next
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
            fill="currentColor"
            className="size-5"
          >
            <path
              fillRule="evenodd"
              d="M8.22 5.22a.75.75 0 0 1 1.06 0l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.75.75 0 0 1-1.06-1.06L11.94 10 8.22 6.28a.75.75 0 0 1 0-1.06Z"
              clipRule="evenodd"
            />
          </svg>
        </button>
      </nav>
    </>
  );
}
