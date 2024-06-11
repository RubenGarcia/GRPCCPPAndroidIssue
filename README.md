# GRPCCPPAndroidIssue
GRPC CPP Android Issue

# Compile for android

The following error appears

```
Building Library\Bee\artifacts\Android\d8kzr\libil2cpp.so failed with output:
ld.lld: error: undefined symbol: stderr
>>> referenced by re2.cc
>>>               re2.cc.o:(LogMessage::Flush()) in archive Assets/GRPC_CPP_Libraries/libre2.a
>>> referenced by re2.cc
>>>               re2.cc.o:(LogMessage::Flush()) in archive Assets/GRPC_CPP_Libraries/libre2.a
>>> referenced by bcm.c
>>>               bcm.c.o:(wait_for_entropy) in archive Assets/GRPC_CPP_Libraries/libcrypto.a
>>> referenced 11 more times

ld.lld: error: undefined symbol: stdout
>>> referenced by stdout_logger.cc
>>>               stdout_logger.cc.o:(grpc_core::experimental::StdoutAuditLogger::Log(grpc_core::experimental::AuditContext const&)) in archive Assets/GRPC_CPP_Libraries/libgrpc_authorization_provider.a
>>> referenced by stdout_logger.cc
>>>               stdout_logger.cc.o:(grpc_core::experimental::StdoutAuditLogger::Log(grpc_core::experimental::AuditContext const&)) in archive Assets/GRPC_CPP_Libraries/libgrpc_authorization_provider.a

ld.lld: error: undefined symbol: __gnu_strerror_r
>>> referenced by strerror.cc
>>>               strerror.cc.o:(grpc_core::StrError(int)) in archive Assets/GRPC_CPP_Libraries/libgpr.a
>>> referenced by strerror.cc
>>>               strerror.cc.o:(absl::lts_20240116::base_internal::StrError(int)) in archive Assets/GRPC_CPP_Libraries/libabsl_strerror.a
>>> referenced by strerror.cc
>>>               strerror.cc.o:(absl::lts_20240116::base_internal::StrError(int)) in archive Assets/GRPC_CPP_Libraries/libabsl_strerror.a

ld.lld: error: undefined symbol: __write_chk
>>> referenced by raw_logging.cc
>>>               raw_logging.cc.o:(absl::lts_20240116::raw_log_internal::AsyncSignalSafeWriteError(char const*, unsigned long)) in archive Assets/GRPC_CPP_Libraries/libabsl_raw_logging_internal.a
>>> referenced by raw_logging.cc
>>>               raw_logging.cc.o:(absl::lts_20240116::raw_log_internal::RawLog(absl::lts_20240116::LogSeverity, char const*, int, char const*, ...)) in archive Assets/GRPC_CPP_Libraries/libabsl_raw_logging_internal.a
clang++: error: linker command failed with exit code 1 (use -v to see invocation)

UnityEngine.GUIUtility:ProcessEvent (int,intptr,bool&)
```
