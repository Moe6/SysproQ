'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic
Imports SysproQ.Entity

Partial Public Class InvMaster
    Public Property StockCode As String
    Public Property Description As String
    Public Property LongDesc As String
    Public Property AlternateKey1 As String
    Public Property AlternateKey2 As String
    Public Property EccUser As String
    Public Property StockUom As String
    Public Property AlternateUom As String
    Public Property OtherUom As String
    Public Property ConvFactAltUom As Decimal
    Public Property ConvMulDiv As String
    Public Property ConvFactOthUom As Decimal
    Public Property MulDiv As String
    Public Property Mass As Decimal
    Public Property Volume As Decimal
    Public Property Decimals As Decimal
    Public Property PriceCategory As String
    Public Property PriceMethod As String
    Public Property Supplier As String
    Public Property CycleCount As Decimal
    Public Property ProductClass As String
    Public Property TaxCode As String
    Public Property OtherTaxCode As String
    Public Property ListPriceCode As String
    Public Property SerialMethod As String
    Public Property InterfaceFlag As String
    Public Property KitType As String
    Public Property LowLevelCode As Decimal
    Public Property Buyer As String
    Public Property Planner As String
    Public Property TraceableType As String
    Public Property MpsFlag As String
    Public Property BulkIssueFlag As String
    Public Property AbcClass As String
    Public Property LeadTime As Decimal
    Public Property StockMovementReq As String
    Public Property ClearingFlag As String
    Public Property SupercessionDate As Nullable(Of Date)
    Public Property AbcAnalysisReq As String
    Public Property AbcCostingReq As String
    Public Property CostUom As String
    Public Property MinPricePct As Decimal
    Public Property LabourCost As Decimal
    Public Property MaterialCost As Decimal
    Public Property FixOverhead As Decimal
    Public Property VariableOverhead As Decimal
    Public Property PartCategory As String
    Public Property DrawOfficeNum As String
    Public Property WarehouseToUse As String
    Public Property BuyingRule As String
    Public Property SpecificGravity As Decimal
    Public Property ImplosionNum As Decimal
    Public Property Ebq As Decimal
    Public Property ComponentCount As Decimal
    Public Property FixTimePeriod As Decimal
    Public Property PanSize As Decimal
    Public Property DockToStock As Decimal
    Public Property OutputMassFlag As String
    Public Property ShelfLife As Decimal
    Public Property Version As String
    Public Property Release As String
    Public Property DemandTimeFence As Decimal
    Public Property MakeToOrderFlag As String
    Public Property ManufLeadTime As Decimal
    Public Property GrossReqRule As String
    Public Property PercentageYield As Decimal
    Public Property AbcPreProd As Decimal
    Public Property AbcManufacturing As Decimal
    Public Property AbcSales As Decimal
    Public Property AbcCumPreProd As Decimal
    Public Property AbcCumManuf As Decimal
    Public Property WipCtlGlCode As String
    Public Property ResourceCode As String
    Public Property GstTaxCode As String
    Public Property PrcInclGst As String
    Public Property SerEntryAtSale As String
    Public Property StpSelection As String
    Public Property UserField1 As String
    Public Property UserField2 As Decimal
    Public Property UserField3 As String
    Public Property UserField4 As String
    Public Property UserField5 As String
    Public Property TariffCode As String
    Public Property SupplementaryUnit As String
    Public Property EbqPan As String
    Public Property StdLandedCost As Decimal
    Public Property LctRequired As String
    Public Property StdLctRoute As String
    Public Property IssMultLotsFlag As String
    Public Property InclInStrValid As String
    Public Property StdLabCostsBill As Decimal
    Public Property PhantomIfComp As String
    Public Property CountryOfOrigin As String
    Public Property StockOnHold As String
    Public Property StockOnHoldReason As String
    Public Property EccFlag As String
    Public Property StockAndAltUm As String
    Public Property AltUnitChar As Decimal
    Public Property JobsOnHold As String
    Public Property JobHoldAllocs As String
    Public Property PurchOnHold As String
    Public Property SalesOnHold As String
    Public Property MaintOnHold As String
    Public Property BatchBill As String
    Public Property BlanketPoExists As String
    Public Property CallOffBpoExists As String
    Public Property DistWarehouseToUse As String
    Public Property JobClassification As String
    Public Property SubContractCost As Decimal
    Public Property DateStkAdded As Nullable(Of Date)
    Public Property InspectionFlag As String
    Public Property SerialPrefix As String
    Public Property SerialSuffix As String
    Public Property ReturnableItem As String
    Public Property ProductGroup As String
    Public Property PriceType As String
    Public Property Basis As String
    Public Property ManualCostFlag As String
    Public Property ManufactureUom As String
    Public Property ConvFactMuM As Decimal
    Public Property ManMulDiv As String
    Public Property LookAheadWin As Decimal
    Public Property LoadingFactor As Decimal
    Public Property SupplUnitCode As String
    Public Property StorageSecurity As String
    Public Property StorageHazard As String
    Public Property StorageCondition As String
    Public Property ProductShelfLife As Decimal
    Public Property InternalShelfLife As Decimal
    Public Property AltMethodFlag As String
    Public Property AltSisoFlag As String
    Public Property AltReductionFlag As String
    Public Property TimeStamp As Byte()
    Public Property WithTaxExpenseType As String

    Public Overridable Property SorDetails As ICollection(Of SorDetail) = New HashSet(Of SorDetail)

End Class
