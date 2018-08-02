
SELECT        TOP (3) [key], link_url, subject, sent_at, sender_email, unique_click_count, total_click_count, bulletins_links_details_Id
FROM            dbo.tblLinkDetails
WHERE        (CHARINDEX('newsletter', subject) > 0)
ORDER BY unique_click_count DESC