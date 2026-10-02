export default function ApartmentCardSkeleton() {
  return (
    <>
      <div className="animate-pulse" aria-hidden>
        {/* Image */}
        <div className="aspect-square rounded-xl bg-gray-200" />

        {/* Content */}
        <div className="mt-3">
          <div className="flex items-center justify-between gap-2">
            <div className="h-5 w-2/3 rounded bg-gray-200" />
            <div className="h-4 w-10 rounded bg-gray-200" />
          </div>

          <div className="mt-2 h-4 w-1/2 rounded bg-gray-200" />

          <div className="mt-2 h-4 w-1/3 rounded bg-gray-200" />
        </div>
      </div>
    </>
  );
}
